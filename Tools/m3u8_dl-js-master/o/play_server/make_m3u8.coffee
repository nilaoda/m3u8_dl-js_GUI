# make_m3u8.coffee, m3u8_dl-js/o/play_server/

# build a m3u8 file (text) from m3u8_dl-js 's .meta.json file
make_m3u8 = (meta) ->
  m = meta.m3u8_info

  o = []
  # m3u8 headers
  o.push '#EXTM3U'
  o.push '#EXT-X-VERSION:3'
  o.push "#EXT-X-TARGETDURATION:#{m.target_duration}"
  # add clips
  for c in m.clip
    o.push "#EXTINF:#{c.time_s},"
    o.push c.name.ts
  # m3u8 end
  o.push '#EXT-X-ENDLIST'

  # done
  o.join('\n') + '\n'

module.exports = make_m3u8
