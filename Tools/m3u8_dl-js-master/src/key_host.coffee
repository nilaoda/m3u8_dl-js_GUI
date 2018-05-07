# key_host.coffee, m3u8_dl-js/src/

async_ = require './async'
util = require './util'
log = require './log'
config = require './config'

dl_with_proxy = require './dl_with_proxy'


_etc = {
  key_info: null

  # loaded KEY (value) list  { key_id: key_value }
  key: {}
}


# download one key file (MUST run in single thread)
_dl_one_key = (key_id) ->
  key = _etc.key_info[key_id]
  # check key method
  if key.method != 'AES-128'
    throw new Error "not support encrypt (KEY) method `#{key.method}`"
  # check base_url
  key_url = util.check_merge_base_url key.uri
  key_file = key.filename  # local file name
  log.d "download key file #{key_file}: #{key_url}"
  await dl_with_proxy key_url, key_file
  # read key
  key_value = await async_.read_file_byte key_file
  # save key (cache key ?)
  _etc.key[key_id] = key_value

_init_key_info = ->
  # reset load key list
  _etc.to_load_list = []
  _etc.flag_load_thread_run = false

# (fake) multi-thread to single-thread (maybe use lock ?)
_load_key = (key_id) ->
  new Promise (resolve, reject) ->
    _put_in_list = ->
      _callback = (err) ->
        if err
          reject err
        else
          resolve()
      one = {
        key_id
        callback: _callback
      }
      _etc.to_load_list.push one

    _check_load_next = ->
      if _etc.to_load_list.length < 1
        # no more keys to load
        _etc.flag_load_thread_run = false  # reset flag
        return  # end this (fake) thread
      [one, _etc.to_load_list] = [ _etc.to_load_list[0], _etc.to_load_list[1..] ]
      # check this key already loaded
      if _etc.key[one.key_id]?
        one.callback()

        # load next key
        setTimeout _check_load_next, 0
        return  # not load one key twice
      # start load this key
      _dl_one_key(one.key_id).catch( (err) ->
        # load one key error
        log.e "load key #{one.key_id} failed ! "
        one.callback err

        # load next key
        _check_load_next()
      ).then () ->
        one.callback()  # load success
        # load next key
        _check_load_next()
    # put task in list
    _put_in_list()
    # check load thread running
    if ! _etc.flag_load_thread_run
      _etc.flag_load_thread_run = true  # set flag
      _check_load_next()  # start load thread


get_key = (key_id) ->
  # check config (command line) first
  key = config.m3u8_key key_id
  if key?
    return key
  # key in m3u8 file
  key = _etc.key[key_id]
  if key?
    return key
  # check key_id
  if ! _etc.key_info[key_id]
    throw new Error "no key info for key #{key_id}"
  # load key
  await _load_key key_id
  # NOW key should be loaded
  get_key key_id  # call self

get_iv = (key_id) ->
  # check config first
  iv = config.m3u8_iv key_id
  if iv?
    return iv
  # iv in m3u8 file
  key = _etc.key_info[key_id]
  if ! key?
    throw new Error "no key info for key #{key_id}"
  key.iv  # may return null (if no IV)

# can be use only once before start download
set_key_info = (key_info) ->
  _etc.key_info = key_info
  _init_key_info()

module.exports = {
  get_key  # async

  get_iv
  set_key_info
}
