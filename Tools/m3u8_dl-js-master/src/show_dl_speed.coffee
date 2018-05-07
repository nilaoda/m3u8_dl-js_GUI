# show_dl_speed.coffee, m3u8_dl-js/src/
path = require 'path'

async_ = require './async'
util = require './util'
log = require './log'
config = require './config'
dl_speed = require './dl_speed'


_p_help = ->
  console.log '''
  show_dl_speed [OPTIONS] [DIR]
  Usage:

    --put-exit-flag    Enable retry function (put flag file)

    --retry-after SEC  Retry after this seconds (default: 10)
    --retry-hide SEC   Hide retry debug info in this seconds (default: 5)
    --init-wait SEC    Wait seconds for init_wait mode (default: 20)

    --version  Show version of this program
    --help     Show this help text
  More information online <https://github.com/sceext2/m3u8_dl-js>
  '''

_p_arg = (args) ->
  rest = args
  _next = ->
    o = rest[0]
    rest = rest[1..]
    o

  o = {}
  while rest.length > 0
    one = _next()
    switch one
      when '--help', '--version'
        o.type = one
      when '--put-exit-flag'
        o.put_exit_flag = true

      when '--retry-after'
        o.retry_after = Number.parseInt _next()
      when '--retry-hide'
        o.retry_hide = Number.parseInt _next()
      when '--init-wait'
        o.init_wait = Number.parseInt _next()

      else  # default: DIR
        config.output_dir one
  o

_etc = {
  # command line arguments
  put_exit_flag: false
  retry_after: 10
  retry_hide: 5
  init_wait: 20

  # show_dl_speed mode
  # + null          Init / exit
  # + 'wait_meta'   Wait meta file to exist
  # + 'wait_lock'   Wait lock file to exist
  # + 'init_wait'   Wait after first lock file exist
  # + 'show_speed'  Normal show download speed mode
  # + 'speed_0'     Download speed is 0
  mode: null

  # print flags
  not_show_speed: false
  # print warnings (only show once)
  show_lock_not_exist: false

  show_will_retry: false

  init_wait_count: 0
  speed_0_count: 0
}

# create `m3u8_dl.exit.flag` file to retry
_put_flag_file = (speed_keep_s) ->
  log.d "now retry (speed 0 for #{speed_keep_s} s ): put exit flag file `#{path.resolve config.EXIT_FLAG_FILE}`"
  await util.write_file config.EXIT_FLAG_FILE, ''  # create null flag file


# TODO re-write show_dl_speed state process with a state machine class
# some reset (mode) functions

_reset_flags = ->
  _etc.mode = null
  _etc.show_lock_not_exist = false

_reset_to_wait_meta = ->
  if _etc.mode is 'wait_meta'
    return  # not reset if already in this mode
  _reset_flags()
  _etc.mode = 'wait_meta'

_reset_to_wait_lock = ->
  if _etc.mode is 'wait_lock'
    return
  _reset_flags()
  _etc.mode = 'wait_lock'
  # set show_lock_not_exist
  _etc.show_lock_not_exist = true

_reset_to_init_wait = ->
  if _etc.mode is 'init_wait'
    return
  _reset_flags()
  _etc.mode = 'init_wait'
  # reset init_wait_count
  _etc.init_wait_count = 0

_reset_to_show_speed = ->
  if _etc.mode is 'show_speed'
    return
  _reset_flags()
  _etc.mode = 'show_speed'

_reset_to_speed_0 = ->
  if _etc.mode is 'speed_0'
    return
  _reset_flags()
  _etc.mode = 'speed_0'
  # reset speed 0 count
  _etc.speed_0_count = 0

# FIXME improve output with speed
_print_enter_init_mode = ->
  log.d "init_wait mode for #{_etc.init_wait} s .. . "

_check_mode = (lock_exist, speed) ->
  # reset flags
  _etc.show_will_retry = false
  # check enable put_exit_flag
  if ! _etc.put_exit_flag
    if lock_exist  # just enter 'show_speed' mode
      _reset_to_show_speed()
    return

  # process mode: 'wait_lock' (and mode null)
  if (_etc.mode is 'wait_lock') || (! _etc.mode?)
    if ! lock_exist
      return  # still wait
    else  # enter 'init_wait' mode
      _reset_to_init_wait()
      # check print debug
      if speed is 0
        _print_enter_init_mode()
  # process mode: 'init_wait'
  if _etc.mode is 'init_wait'
    # check speed
    if speed is 0
      _etc.init_wait_count += 1
      if _etc.init_wait_count < _etc.init_wait
        return  # still wait
      else
        log.d "exit init_wait mode (timeout after #{_etc.init_wait_count} s)"
    else
      log.d "exit init_wait mode (speed > 0)"
    # enter 'show_speed' mode
    _reset_to_show_speed()
  # NOTE there is no 'wait_exit' mode

  # check speed
  if speed is 0
    _reset_to_speed_0()

    _etc.speed_0_count += 1
    if _etc.speed_0_count > _etc.retry_after
      await _put_flag_file _etc.speed_0_count
      # just enter init_wait mode
      _reset_to_init_wait()
      # FIXME improve output with speed check
      _print_enter_init_mode()
    else if _etc.speed_0_count > _etc.retry_hide
      _etc.show_will_retry = true
  else
    _reset_to_show_speed()

_show_speed = (speed) ->
  # check and print dl speed
  time_left = dl_speed.get_time_left()
  if (speed > 0) || time_left?  # time_left is not `unknow`
    _etc.not_show_speed = false  # reset flag
  else if _etc.not_show_speed
    return
  # not show 0 speed in these mode
  else if ['wait_meta', 'init_wait'].indexOf(_etc.mode) != -1
    return
  else
    _etc.not_show_speed = true
  console.log dl_speed.print_speed()

_update = ->
  # check meta file exist
  if ! await async_.file_exist(config.META_FILE)
    _reset_to_wait_meta()
    log.d "waiting for #{path.resolve config.META_FILE}"
    return
  # check lock file exist
  if ! await async_.file_exist(config.LOCK_FILE)
    _reset_to_wait_lock()
  else
    lock_exist = true
  # update scan
  await dl_speed.load_meta_file()
  exit_flag = await dl_speed.update()
  speed = dl_speed.get_dl_speed()
  if (! speed?) || (speed < 1)
    speed = 0
  else  # speed is not 0
    _etc.speed_0_count = 0  # reset speed_0_count
  # process mode
  await _check_mode lock_exist, speed

  _show_speed()
  # check and print warnings
  if _etc.not_show_speed  # print warnings only after not show 0 speed
    if _etc.show_lock_not_exist
      _etc.show_lock_not_exist = false  # only show once
      log.w "lock file `#{path.resolve config.LOCK_FILE}` not exist. m3u8_dl-js NOT running ?"
  # check print will retry
  if _etc.show_will_retry
    log.d "will retry after #{_etc.retry_after - _etc.speed_0_count} s (speed 0) .. . "
  # done
  exit_flag

_normal = (a) ->
  # save options
  if a.put_exit_flag
    _etc.put_exit_flag = true
  if a.retry_after?
    _etc.retry_after = a.retry_after
  if a.retry_hide?
    _etc.retry_hide = a.retry_hide
  if a.init_wait?
    _etc.init_wait = a.init_wait
  # change cwd
  await util.check_change_cwd()
  log.d "working directory #{process.cwd()}"
  # init scan
  if await async_.file_exist config.META_FILE
    await dl_speed.load_meta_file()
    exit_flag = await dl_speed.update()
    if exit_flag
      console.log dl_speed.print_speed()
      return  # task done, not enter main loop
  # main loop
  while true
    # sleep before scan again
    await async_.sleep dl_speed.UPDATE_TIME  # sleep 1s
    if await _update()
      break  # exit

main = (argv) ->
  a = _p_arg argv
  # check start type
  switch a.type
    when '--help'
      _p_help()
    when '--version'
      util.p_version()
    else
      await _normal a

_start = ->
  try
    await main(process.argv[2..])
  catch e
    # DEBUG
    console.log "ERROR: #{e.stack}"
    #throw e
    process.exit 1
_start()
