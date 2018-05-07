# config.coffee, m3u8_dl-js/src/

# m3u8_dl program version
P_VERSION = 'm3u8_dl-js version 0.3.0-5 test20170614 2148'


# local file struct
LOCK_FILE = 'm3u8_dl.lock'
META_FILE = 'm3u8_dl.meta.json'
EXIT_FLAG_FILE = 'm3u8_dl.exit.flag'  # exit download process if this file exist
RAW_M3U8 = 'raw.m3u8'
RAW_KEY = ['raw.', '.key']  # the key for m3u8 (support multi-keys)
LIST_FILE = 'ffmpeg_merge.list'
# clip file
CLIP_SUFFIX_DL_PART = '.ts.part'
CLIP_SUFFIX_ENCRYPTED = '.ts.encrypted'
CLIP_SUFFIX_TS = '.ts'
# TODO support not '.ts' in m3u8 ?

# auto retry
DEFAULT_RETRY_TIMES = 3
DEFAULT_RETRY_SLEEP = 1


_etc = {
  # support multi-keys
  m3u8_key: {}
  m3u8_iv: {}
}

# get / set proxy
proxy = (p) ->
  if p?
    _etc.proxy = p
  _etc.proxy

m3u8_base_url = (base) ->
  if base?
    _etc.m3u8_base_url = base
  _etc.m3u8_base_url

m3u8_key = (i, k) ->
  if k?
    _etc.m3u8_key[i] = k
  _etc.m3u8_key[i]

m3u8_iv = (i, iv) ->
  if iv?
    _etc.m3u8_iv[i] = iv
  _etc.m3u8_iv[i]

get_all_m3u8_key = ->
  _etc.m3u8_key

get_all_m3u8_iv = ->
  _etc.m3u8_iv

dl_thread = (t) ->
  if t?
    _etc.dl_thread = t
  _etc.dl_thread

# download files to (default current dir)
output_dir = (d) ->
  if d?
    _etc.output_dir = d
  _etc.output_dir

auto_remove = (a) ->
  if a?
    _etc.auto_remove = a
  _etc.auto_remove

# TODO support set different headers for different request ?
headers = (h) ->
  if h?
    _etc.headers = h
  _etc.headers

exit_on_flag = (f) ->
  if f?
    _etc.exit_on_flag = f
  _etc.exit_on_flag

lock_file_fd = (fd) ->
  if fd?
    _etc.lock_file_fd = fd
  _etc.lock_file_fd

curl_bin = (bin) ->
  if bin?
    _etc.curl_bin = bin
  _etc.curl_bin


module.exports = {
  P_VERSION

  LOCK_FILE
  META_FILE
  EXIT_FLAG_FILE
  RAW_M3U8
  RAW_KEY
  LIST_FILE

  CLIP_SUFFIX_DL_PART
  CLIP_SUFFIX_ENCRYPTED
  CLIP_SUFFIX_TS

  DEFAULT_RETRY_TIMES
  DEFAULT_RETRY_SLEEP


  proxy  # get / set
  m3u8_base_url  # get /set
  m3u8_key  # get / set
  m3u8_iv  # get / set
  get_all_m3u8_key
  get_all_m3u8_iv

  dl_thread  # get / set
  output_dir  # get / set
  auto_remove  # get / set
  headers  # get / set

  exit_on_flag  # get / set
  lock_file_fd  # get / set

  curl_bin  # get / set
}
