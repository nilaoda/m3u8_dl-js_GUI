# parse_m3u8.coffee, m3u8_dl-js/src/
log = require './log'


_split_lines = (raw_text) ->
  lines = raw_text.split '\n'
  o = []
  for l in lines
    l = l.trim()
    if l != ''
      o.push l
  o

_parse_one_key = (raw_line) ->
  # support key line: `#EXT-X-KEY:METHOD=AES-128,URI="http://XXXX.key"`

  # TODO support parse IV
  # TODO support more format parse
  o = {}
  rest = raw_line
  M = 'METHOD='
  if rest.startsWith M
    rest = rest[M.length ..]
    i = rest.indexOf(',')
    if i is -1
      o.method = rest
    else
      o.method = rest[0...i]
      rest = rest[(i + 1)..]

      U = 'URI='
      if rest.startsWith U
        rest = rest[U.length ..]
        if rest.startsWith '"'
          o.uri = rest[1..]
          if o.uri.endsWith '"'
            o.uri = o.uri[0... o.uri.length - 1]
        else
          o.uri = rest
      # TODO maybe more process
  # else: TODO unknow format
  o

# support multi-keys
_parse_key = (key_info, raw_line) ->
  one = _parse_one_key raw_line
  # check key_id
  if key_info.key_id?
    key_info.key_id += 1
  else  # init key_id
    key_info.key_id = 0
    key_info.key = {}
  # save key info
  key_info.key[key_info.key_id] = one


parse_m3u8 = (raw_m3u8_text) ->
  M3U  = '#EXTM3U'
  V    = '#EXT-X-VERSION:'
  MS   = '#EXT-X-MEDIA-SEQUENCE:'
  TD   = '#EXT-X-TARGETDURATION:'
  K    = '#EXT-X-KEY:'
  INFO = '#EXTINF:'
  END  = '#EXT-X-ENDLIST'

  line = _split_lines raw_m3u8_text
  # check format
  if line[0] != M3U
    log.w "parse_m3u8: file format is not `#EXTM3U` "

  media_sequence = 0
  clip_s = -1  # clip time_s
  # support multi-keys
  key_info = {
    key_id: null
    key: null
  }

  o = {
    key: null
    clip: []
  }
  for l in line
    if l.startsWith '#'
      if l.startsWith INFO
        clip_s = JSON.parse l[INFO.length ..].split(',', 1)[0]
      if l.startsWith V
        o.version = l[V.length ..]
      else if l.startsWith MS
        media_sequence = Number.parseInt l[MS.length ..]
      else if l.startsWith TD
        o.target_duration = l[TD.length ..]
      else if l.startsWith K
        _parse_key key_info, l[K.length ..]
      else if l is END
        o.key = key_info.key  # add key info
        return o  # got file end
      # else: ignore this line
    else
      # is a clip file line
      one = {
        media_sequence
        url: l
        time_s: clip_s
      }
      # add key_id
      if key_info.key_id?
        one.key_id = key_info.key_id
      o.clip.push one

      clip_s = -1  # reset clip time_s
      media_sequence += 1
  # no `#EXT-X-ENDLIST`
  log.w "parse_m3u8: not found m3u8 end `#EXT-X-ENDLIST` "
  o.key = key_info.key
  o

module.exports = parse_m3u8
