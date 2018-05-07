# do_dl.coffee, m3u8_dl-js/src/
path = require 'path'
fs = require 'fs'

async_ = require './async'
util = require './util'
log = require './log'
config = require './config'
key_host = require './key_host'
dl_speed = require './dl_speed'

parse_m3u8 = require './parse_m3u8'
dl_clip = require './dl_clip'
thread_pool = require './thread_pool'


_create_meta_file = (m3u8, m3u8_info) ->
  # support multi-keys
  _save_key = ->
    o = {
      m3u8_key: {}
      m3u8_iv: {}
    }
    keys = config.get_all_m3u8_key()
    ivs = config.get_all_m3u8_iv()

    for i of keys
      o.m3u8_key[i] = keys[i].toString 'base64'
    for i of ivs
      o.m3u8_iv[i] = ivs[i].toString 'base64'
    o

  o = {
    p_version: config.P_VERSION
    m3u8: m3u8
    cwd: process.cwd()
    # save base url
    m3u8_base_url: config.m3u8_base_url()
    # save KEY and IV in meta file
    decrypt: _save_key()

    m3u8_info: m3u8_info
    last_update: util.last_update()
  }
  text = util.print_json(o) + '\n'
  await util.write_file config.META_FILE, text

_make_filename = (m3u8_info) ->
  _add_zero = (raw, len) ->
    while raw.length < len
      raw = '0' + raw
    raw
  len = m3u8_info.clip.length.toString().length

  _one_filename = (index) ->
    base = _add_zero('' + index, len)
    # output
    {
      part: base + config.CLIP_SUFFIX_DL_PART
      encrypted: base + config.CLIP_SUFFIX_ENCRYPTED
      ts_tmp: base + config.CLIP_SUFFIX_TS + util.WRITE_REPLACE_FILE_SUFFIX
      ts: base + config.CLIP_SUFFIX_TS
    }
  for i in [0... m3u8_info.clip.length]
    c = m3u8_info.clip[i]
    c.name = _one_filename i
  # make key filename (multi-key support)
  if m3u8_info.key?
    key = m3u8_info.key
    key_count = Object.keys(key).length

    len = key_count.toString().length
    N = config.RAW_KEY
    for i of key
      key[i].filename = N[0] + _add_zero(i, len) + N[1]
    # add key_count
    m3u8_info.key_count = key_count
  # add clip_count
  m3u8_info.clip_count = m3u8_info.clip.length
  m3u8_info

# check and process clip base url
_check_clip_base_url = (m3u8_info) ->
  # DEBUG
  if config.m3u8_base_url()?
    log.d "base URL #{config.m3u8_base_url()}"
  for c in m3u8_info.clip
    c.clip_url = util.check_merge_base_url c.url
  m3u8_info

# create ffmpeg merge list
_create_list_file = (m3u8_info) ->
  o = []
  for c in m3u8_info.clip
    o.push "file \'#{c.name.ts}\'"
  text = o.join('\n') + '\n'
  # write list file
  await util.write_file config.LIST_FILE, text

_create_lock = ->
  # check lock file
  fd = await util.create_lock_file config.LOCK_FILE
  config.lock_file_fd fd

  _remove_lock = ->
    # close file before remove it
    try
      fs.closeSync fd
    catch e
      # ignore error
    try
      fs.unlinkSync config.LOCK_FILE
    catch e
      # ignore if file not exist
    # check lock file exist
    if fs.existsSync config.LOCK_FILE
      log.e "can not remove LOCK file: #{path.resolve config.LOCK_FILE}"
  # remove LOCK on process exit
  process.on 'exit', _remove_lock
  # FIXME other ways to exit this process
  process.on 'SIGINT', () ->
    log.d "recv SIGINT, exiting .. . "
    process.exit 0

_check_exit_on_flag = ->
  _enable_exit_on_flag = ->
    while true
      # sleep 1s
      await async_.sleep 1e3
      # check flag file exist
      if await async_.file_exist config.EXIT_FLAG_FILE
        # try to remove it first
        await async_.rm config.EXIT_FLAG_FILE
        # re-check
        if await async_.file_exist config.EXIT_FLAG_FILE
          log.w "can not remove FLAG file `#{path.resolve config.EXIT_FLAG_FILE}`, not exit"
        else
          log.d "flag file `#{path.resolve config.EXIT_FLAG_FILE}` exist, exiting .. . "
          process.exit 1  # do exit
  if config.exit_on_flag()
    log.d "enable exit_on_flag"
    _enable_exit_on_flag()


_count_clip_time = (m3u8_info) ->
  o = 0
  for i in m3u8_info.clip
    t = i.time_s
    if t >= 0
      o += t
  o

_download_clips = (m3u8_info) ->
  # single-thread mode: just use loop
  _single_thread = ->
    for i in [0... m3u8_info.clip.length]
      await dl_clip m3u8_info, i
  # use thread_pool
  _multi_thread = (n) ->
    _worker = (i) ->
      await dl_clip m3u8_info, i
    # create task list
    t = []
    for i in [0... m3u8_info.clip.length]
      t.push i
    pool = thread_pool n
    await pool.run t, _worker

  video_time = dl_speed.print_time _count_clip_time(m3u8_info)
  log.d "download #{m3u8_info.clip.length} clips, video time #{video_time} "
  # check number of download thread
  thread_n = config.dl_thread()
  if (! thread_n?) || (thread_n <= 1)
    await _single_thread()
  else
    log.d "  with #{thread_n} threads "
    await _multi_thread thread_n


do_dl = (m3u8) ->
  # DEBUG
  if config.proxy()?
    log.d "use proxy #{JSON.stringify config.proxy()}"
  # check is remote file (http) or local file
  if m3u8.startsWith('http://') || m3u8.startsWith('https://')
    # remote file
    if ! config.m3u8_base_url()?  # not override command line
      config.m3u8_base_url util.get_base_url(m3u8)  # set base_url
    # change working directory now
    await util.check_change_cwd true
    await _create_lock()
    # download that m3u8 file
    log.d "download m3u8 file #{m3u8}"
    dl_tmp_file = config.RAW_M3U8 + util.WRITE_REPLACE_FILE_SUFFIX
    await dl_clip.dl_one m3u8, dl_tmp_file
    await async_.mv dl_tmp_file, config.RAW_M3U8
    # read that text
    m3u8_text = await async_.read_file config.RAW_M3U8
  else  # local file
    log.d "local m3u8 file #{path.resolve m3u8}"
    m3u8_text = await async_.read_file m3u8
    # change working directory here
    await util.check_change_cwd true
    await _create_lock()
    # create raw m3u8 file
    await util.write_file config.RAW_M3U8, m3u8_text
  # parse m3u8 text, and create meta file
  m3u8_info = parse_m3u8 m3u8_text
  # create clip filename
  m3u8_info = _make_filename m3u8_info
  m3u8_info = _check_clip_base_url m3u8_info

  await _create_meta_file m3u8, m3u8_info
  await _create_list_file m3u8_info
  # DEBUG output: key_count
  key_count = m3u8_info.key_count
  if key_count? && (key_count > 0)
    log.d " #{key_count} keys in m3u8 file "
  # set key_info to key_host before start download
  key_host.set_key_info m3u8_info.key

  # support exit_on_flag
  _check_exit_on_flag()

  await _download_clips m3u8_info
  # not DEBUG
  log.p "[ OK ] all download done. "


module.exports = do_dl  # async
