# dl_clip.coffee, m3u8_dl-js/src/

fs = require 'fs'

async_ = require './async'
log = require './log'
config = require './config'
decrypt = require './decrypt'
key_host = require './key_host'

dl_with_proxy = require './dl_with_proxy'
dl_with_curl = require './dl_with_curl'


_decrypt_clip = (clip) ->
  # support multi-keys
  _do_decrypt = (c, name) ->
    new Promise (resolve, reject) ->
      r = fs.createReadStream name.encrypted
      w = fs.createWriteStream name.ts_tmp
      r.pipe(c).pipe(w)
      r.on 'error', (err) ->
        reject err
      c.on 'error', (err) ->
        reject err
      w.on 'error', (err) ->
        reject err
      w.on 'finish', () ->
        resolve()
  before_size = await async_.get_file_size clip.name.encrypted

  key_id = clip.key_id
  iv = config.m3u8_iv key_id
  if ! iv?
    iv = clip.media_sequence
  key = await key_host.get_key key_id

  c = decrypt.create_decrypt_stream(key, iv)
  await _do_decrypt c, clip.name

  after_size = await async_.get_file_size clip.name.ts_tmp
  # check decrypt success (by file_size)
  if ! (after_size < before_size)  # after remove padding
    log.w "#{clip.name.ts}: decrypt MAY fail !  (before_size = #{before_size}, after_size = #{after_size}) "
  else  # check auto remove
    if config.auto_remove()
      log.d "auto remove #{clip.name.encrypted}"
      await async_.rm clip.name.encrypted

dl_one = (file_url, filename) ->
  # check use which downloader
  if config.curl_bin()?
    await dl_with_curl file_url, filename
  else
    await dl_with_proxy file_url, filename

# support multi-keys
dl_clip = (m3u8_info, index) ->
  clip = m3u8_info.clip[index]
  # check already exist and skip it
  if await async_.file_exist(clip.name.ts)
    log.d "dl_clip: skip exist file #{clip.name.ts}"
    return
  # load key before download clip
  if clip.key_id?
    await key_host.get_key clip.key_id
  # download file (support proxy)
  try
    clip_url = clip.clip_url
    # DEBUG
    log.d "dl_clip: #{clip.name.ts}: #{clip_url}"
    await dl_one clip_url, clip.name.part
  catch e
    log.e "dl_clip: #{clip.name.ts}: download error ! "
    # print stack in multi-thread mode
    if config.dl_thread()? && (config.dl_thread() > 1)
      console.log e.stack
    throw e
  # check need decrypt clip
  if clip.key_id?
    # download one file done, rename it
    await async_.mv clip.name.part, clip.name.encrypted
    await _decrypt_clip clip
    await async_.mv clip.name.ts_tmp, clip.name.ts
  else  # no need to decrypt
    await async_.mv clip.name.part, clip.name.ts
  # download one clip done

dl_clip.dl_one = dl_one  # async
module.exports = dl_clip  # async
