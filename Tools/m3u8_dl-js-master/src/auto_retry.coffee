# auto_retry.coffee, m3u8_dl-js/src/
path = require 'path'

async_ = require './async'
util = require './util'
log = require './log'
config = require './config'
dl_speed = require './dl_speed'


_p_help = ->
  console.log '''
  auto_retry [OPTIONS] M3U8 -- OPTIONS_FOR_M3U8_DL
  Usage:
    -o, --output DIR  Download files to this Directory

        --retry NUM     Retry times (default: 3)
        --use-raw-m3u8  Use `raw.m3u8` file for retry
        --sleep SEC     Sleep seconds before next retry (default: 1)

        --remove-part-files  Remove all `.part` files before run m3u8_dl-js

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

  o = {
    pass: []  # arguments passed to m3u8_dl
  }
  start_pass = false
  while rest.length > 0
    one = _next()
    if start_pass
      o.pass.push one
      continue
    switch one
      when '--help', '--version'
        o.type = one
      when '-o', '--output'
        config.output_dir _next()

      when '--retry'
        o.retry = Number.parseInt _next()
        # NaN for retry Infinity
        if o.retry < 1
          throw new Error "bad retry times #{o.retry}"
      when '--sleep'
        o.retry_sleep = JSON.parse _next()
        if (typeof o.retry_sleep != 'number') || Number.isNaN(o.retry_sleep) || (o.retry_sleep < 0)
          throw new Error "bad retry sleep #{o.retry_sleep}"
      when '--use-raw-m3u8'
        o.use_raw_m3u8 = true
      when '--remove-part-files'
        o.remove_part_files = true
      when '--'
        start_pass = true

      else  # default: M3U8
        o.m3u8 = one
  o

# for _normal tasks

_etc = {
  pass: []
  retry: config.DEFAULT_RETRY_TIMES
  retry_sleep: config.DEFAULT_RETRY_SLEEP
  use_raw_m3u8: false
  remove_part_files: false
  m3u8: null

  retry_count: 0
  last_clip_done: 0

  all_retry_count: 0  # not reset this count
}

M3U8_DL_BIN = './m3u8_dl.js'
_run_m3u8_dl = (args) ->
  # check lock file before run m3u8_dl
  if await async_.file_exist config.LOCK_FILE
    throw new Error "lock file `#{path.resolve config.LOCK_FILE}` already exist "
  # check remove_part_files
  if _etc.remove_part_files && (await async_.file_exist(config.META_FILE))
    log.d "AUTO_RETRY: remove all `.part` files .. . "
    await dl_speed.load_meta_file()
    clip = dl_speed.get_meta().m3u8_info.clip
    for c in clip
      if await async_.file_exist c.name.part
        await async_.rm c.name.part

  node = process.argv[0]
  m3u8_dl = path.join __dirname, M3U8_DL_BIN

  arg = [node, m3u8_dl].concat args
  await async_.run_cmd arg

# return `true` to exit
_retry_loop_one = ->
  # check meta file exist
  if ! await async_.file_exist config.META_FILE
    log.w "AUTO_RETRY: meta file `#{path.resolve config.META_FILE}` not exist ! "
    meta_exist = false
    task_done = false
    cost_retry = true
  else  # load meta file and check task finished
    await dl_speed.load_meta_file()
    task_done = await dl_speed.update()

    meta_exist = true
    # check cost retry
    clip_count = dl_speed.get_clip_count()
    # DEBUG
    log.d "AUTO_RETRY: clip count: #{clip_count.done}/#{clip_count.all}  (last #{_etc.last_clip_done} done)"
    clip_done = clip_count.done
    if clip_done > _etc.last_clip_done
      cost_retry = false
    else
      cost_retry = true
    _etc.last_clip_done = clip_done  # update clip done
  # check task done
  if task_done
    log.p "AUTO_RETRY: [ OK ] task done. (retry #{_etc.all_retry_count} times)"
    return true
  # check cost retry
  if ! cost_retry
    log.d "AUTO_RETRY: reset retry count "
    _etc.retry_count = 0
  else
    if _etc.retry_count >= _etc.retry
      log.e "AUTO_RETRY: give up after retry #{_etc.retry_count} times "
      # TODO process.exit(1) ?
      return true
    else
      log.d "AUTO_RETRY: retry #{_etc.retry_count}/#{_etc.retry}"
  # inc retry count
  if cost_retry
    _etc.retry_count += 1
  _etc.all_retry_count += 1
  # retry sleep
  if _etc.retry_sleep > 0
    log.d "AUTO_RETRY: sleep #{_etc.retry_sleep} seconds before next retry "
    await async_.sleep(_etc.retry_sleep * 1e3)
  # retry: check use_raw_m3u8
  if (! meta_exist) || (! _etc.use_raw_m3u8)
    arg = _etc.pass.concat [_etc.m3u8]
  else
    log.d "AUTO_RETRY: enable use_raw_m3u8 "

    base_url = dl_speed.get_meta().m3u8_base_url
    if base_url?
      arg = ['--m3u8-base-url', base_url, config.RAW_M3U8]
    else
      arg = [config.RAW_M3U8]
  await _run_m3u8_dl _etc.pass.concat(arg)
  # not exit
  false

_normal = (a) ->
  # change cwd
  await util.check_change_cwd true  # create dir if not exist
  # save arguments
  _etc.pass = a.pass
  if a.retry?
    _etc.retry = a.retry
  if a.retry_sleep?
    _etc.retry_sleep = a.retry_sleep
  _etc.use_raw_m3u8 = a.use_raw_m3u8
  _etc.remove_part_files = a.remove_part_files
  _etc.m3u8 = a.m3u8

  # run m3u8_dl for the first time
  arg = _etc.pass.concat [_etc.m3u8]
  log.d "AUTO_RETRY: first run m3u8_dl "
  await _run_m3u8_dl arg
  # enter retry loop
  while true
    if await _retry_loop_one()
      break

main = (argv) ->
  try
    a = _p_arg argv
  catch e
    util.p_bad_command_line()
    process.exit 1  # bad command line
  switch a.type
    when '--help'
      _p_help()
    when '--version'
      util.p_version()
    else
      await _normal a
      # FIX process will not exit
      process.exit 0

_start = ->
  try
    await main(process.argv[2..])
  catch e
    # DEBUG
    console.log "ERROR: #{e.stack}"
    #throw e
    process.exit 1
_start()
