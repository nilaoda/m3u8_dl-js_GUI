# dl_speed.coffee, m3u8_dl-js/src/

async_ = require './async'
util = require './util'
log = require './log'
config = require './config'


# global config
UPDATE_TIME = 1e3  # refresh every 1s (1000 ms)

_TIME_LEFT_SPEED = 5  # use 5s 's speed to calc time_left


_etc = {
  # cache `.ts` file size
  ts_file_size: {}

  # meta file data
  meta: null

  # speed info for print
  speed_info: {
    percent: 0  # 0: 0.0 % ; 1: 100%
    dl_speed: 0  # unit: Byte
    clip_count: {
      done: 0
      doing: 0
      all: 0
    }
    dl_size: 0  # downloaded size (unit: Byte )
    all_size: null  # unit: Byte ( `null` for unknow)
    time_left: null  # unit: second ( `null` for unknow)
  }

  # history data (to make speed_info)
  last_dl_size: [ 0 ]
}


load_meta_file = ->
  text = await async_.read_file config.META_FILE
  _etc.meta = JSON.parse text
  # check meta.p_version
  if _etc.meta.p_version != config.P_VERSION
    log.w "META file version mismatch: #{_etc.meta.p_version}"


_count_file_size = ->
  # get file list (current dir)
  l = await async_.list_dir '.'
  fl = {}
  for i in l
    fl[i] = true
  # scan clip file (in meta) and get file size
  m = _etc.meta.m3u8_info
  o = {
    ts: {  # count of `.ts` files
      count: 0  # number of `.ts` files
      size: 0  # files size in Byte
      time_s: 0  # sum of clip time in second
    }
    encrypted: {  # count of `.encrypted` files (not include `.ts` file)
      count: 0
      size: 0
    }
    part: {  # `.part` files
      count: 0
      size: 0
    }
    time_s: 0  # count of all clips
    count: m.clip.length
  }
  _count_one = (clip) ->
    # count clip time_s
    clip_time_s = clip.time_s
    if ! (clip_time_s >= 0)
      clip_time_s = 0
    o.time_s += clip_time_s
    # check `.ts` file
    ts = clip.name.ts
    if fl[ts]  # `.ts` file exist
      # cache `.ts` file size
      if ! _etc.ts_file_size[ts]?
        _etc.ts_file_size[ts] = await async_.get_file_size ts
      size = _etc.ts_file_size[ts]

      o.ts.count += 1
      o.ts.size += size
      o.ts.time_s += clip_time_s
      return
    # check `.encrypted` file
    en = clip.name.encrypted
    if fl[en]
      size = await async_.get_file_size en

      o.encrypted.count += 1
      o.encrypted.size += size
      return
    # check `.part` file
    pa = clip.name.part
    if fl[pa]
      size = await async_.get_file_size pa

      o.part.count += 1
      o.part.size += size
      return
  # count each clip
  for c in m.clip
    await _count_one c
  o

_calc_speed_info = (fl) ->
  exit_flag = false

  si = _etc.speed_info
  # percent
  if fl.ts.count is fl.count
    si.percent = 1  # download done
    exit_flag = true
  else
    si.percent = fl.ts.count / fl.count
  # clip count
  si.clip_count.done = fl.ts.count
  si.clip_count.doing = fl.part.count + fl.encrypted.count
  si.clip_count.all = fl.count
  # dl_size
  si.dl_size = fl.ts.size + fl.encrypted.size + fl.part.size
  # dl_speed
  si.dl_speed = si.dl_size - _etc.last_dl_size[0]
  # process history
  _etc.last_dl_size = [si.dl_size].concat _etc.last_dl_size
  if _etc.last_dl_size.length > (_TIME_LEFT_SPEED + 1)
    _etc.last_dl_size.pop()
  # FIX last_dl_size (last 0 item) (fix calc download speed, time_left)
  if (_etc.last_dl_size.length > 1) && (_etc.last_dl_size[_etc.last_dl_size.length - 1] is 0)
    _etc.last_dl_size.pop()
  # all_size
  if fl.ts.count < 1
    si.all_size = null
  else if fl.ts.count is fl.count
    si.all_size = fl.ts.size
  else
    # calc average bitrate
    br = fl.ts.size / fl.ts.time_s
    si.all_size = br * fl.time_s
  # time_left
  dl_delta = si.dl_size - _etc.last_dl_size[_etc.last_dl_size.length - 1]
  if dl_delta < 1
    avg_speed = null
  else
    avg_speed = dl_delta / (_etc.last_dl_size.length - 1)
  if (! si.all_size?) || (! avg_speed?)
    si.time_left = null
  else
    rest_byte = si.all_size - si.dl_size
    if rest_byte <= 0
      si.time_left = 0
    else
      si.time_left = rest_byte / avg_speed
  # done
  exit_flag

update = ->
  fl = await _count_file_size()
  _calc_speed_info fl  # return exit_flag ( `true` to exit)


# for print_speed

# right indent
_r = (o, len) ->
  while o.length < len
    o = ' ' + o
  o

_print_percent = (percent) ->
  if percent is 1
    o = '100'
  else
    o = (percent * 1e2).toFixed 1
  o += ' %'
  # right indent self
  _r o, 6  # '99.9 %', '100 %'

_print_dl_speed = (speed) ->
  U = 1024
  if speed <= U  # < 1 KB/s
    o = speed + ' Byte/s'
  else if speed <= (U * U)  # < 1 MB/s
    o = Math.round(speed / U) + ' KB/s'
  else if speed <= (U * U * U)  # < 1 GB/s
    o = (speed / (U * U)).toFixed(1) + ' MB/s'
  else
    o = (speed / (U * U * U)).toFixed(1) + ' GB/s'
  # right indent self
  _r o, 11  # '1024 Byte/s', '1024 KB/s', '1023.9 MB/s'

_print_clip_count = (c) ->
  o = "(#{c.done}/#{c.doing}/#{c.all})"
  # right indent self
  _r o, c.all.toString().length * 3 + ('(//)').length

_print_dl_size_all = (dl_size, all_size) ->
  U = 1024
  # dl_size
  if dl_size < 1
    dl = '0'
  else if dl_size <= U  # < 1 KB
    dl = dl_size + ' Byte'
  else if dl_size <= (U * U)  # < 1 MB
    dl = Math.round(dl_size / U) + ' KB'
  else if dl_size <= (U * U * U)  # < 1 GB
    dl = (dl_size / (U * U)).toFixed(1) + ' MB'
  else
    dl = (dl_size / (U * U * U)).toFixed(1) + ' GB'
  # all_size
  if (! all_size?) || (all_size < (U * U))  # < 1 MB
    all = 'unknow'
  else if all_size is dl_size
    all = dl
  else if all_size < (U * U * U)  # < 1 GB
    all = Math.round(all_size / (U * U)).toFixed(1) + ' MB'
  else
    all = (all_size / (U * U * U)).toFixed(1) + ' GB'
  o = dl + '/ ' + all
  # indent self  (9 + 2 + 9)
  _r o, 20  # '0', '1024 Byte', '1024 KB', '1023.9 MB'; 'unknow', '1023.9 MB'

print_time = (time_s) ->
  _add_zero = (raw) ->
    o = raw.toString()
    if o.length < 2
      o = '0' + o
    o

  if ! time_s?
    o = 'UNKNOW'
  else
    if time_s != 0
      time_s = Number.parseInt(time_s) + 1
    m = Number.parseInt(time_s / 60)
    s = time_s - m * 60  # second
    h = Number.parseInt(m / 60)  # hour
    m -= h * 60  # minute

    o = _add_zero(m) + ':' + _add_zero(s)
    if h > 0
      o = _add_zero(h) + ':' + o
  o

_print_rest_time = (time_s) ->
  # indent self
  _r print_time(time_s), 8  # '00:00:00', 'UNKNOW'

print_speed = ->
  si = _etc.speed_info

  prefix = '->'
  percent = _print_percent si.percent
  speed = _print_dl_speed si.dl_speed
  clip = _print_clip_count si.clip_count
  dl_all = _print_dl_size_all si.dl_size, si.all_size
  time = _print_rest_time si.time_left

  # TODO print clip_time/all_time ?

  # output line
  "#{prefix} #{percent} #{speed} #{clip} #{dl_all} #{time}"

# exports function

get_dl_speed = ->
  _etc.speed_info.dl_speed

get_clip_count = ->
  _etc.speed_info.clip_count

get_time_left = ->
  _etc.speed_info.time_left

get_meta = ->
  _etc.meta

module.exports = {
  UPDATE_TIME

  load_meta_file  # async
  update  # async

  print_speed
  print_time

  get_dl_speed
  get_clip_count
  get_time_left
  get_meta
}
