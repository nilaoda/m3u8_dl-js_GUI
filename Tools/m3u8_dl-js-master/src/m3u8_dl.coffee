# m3u8_dl.coffee, m3u8_dl-js/src/
path = require 'path'

async_ = require './async'
util = require './util'
log = require './log'
config = require './config'

do_dl = require './do_dl'


_p_help = ->
  console.log '''
  m3u8_dl-js [OPTIONS] M3U8
  Usage:
    -o, --output DIR  Download files to this Directory

    -T, --thread NUM            Set number of download thread (default: 1)
        --auto-remove           Remove raw file after decrypt success
        --exit-on-flag          Exit when FLAG file exist
    -H, --header NAME:VALUE     Set http header (can use more than once)
        --proxy-http IP:PORT    Set http proxy
        --proxy-socks5 IP:PORT  Set socks5 proxy
        --dl-with-curl CURL     Download clips with cURL
        --m3u8-base-url URL     Set base URL of the m3u8 file

    Set KEY (and IV) for AES-128 decrypt. Use HEX format, base64 format,
    or local binary file. Use ID to set multi-keys.

        --m3u8-key         [ID:]HEX
        --m3u8-iv          [ID:]HEX
        --m3u8-key-base64  [ID:]BASE64
        --m3u8-iv-base64   [ID:]BASE64
        --m3u8-key-file    [ID::]FILE
        --m3u8-iv-file     [ID::]FILE

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

  _split_ip_port = (raw) ->
    p = raw.split ':'
    o = {
      hostname: p[0]
      port: Number.parseInt p[1]
    }

  headers = {}
  _set_header = (raw) ->
    name = raw.split(':', 1)[0]
    value = raw[(name.length + 1) ..]
    headers[name] = value

  # support multi-keys
  key_file_list = []
  iv_file_list = []
  _set_key_iv = (key_iv, format, value) ->
    if format is 'file'
      key_id = 0  # default key_id
      i = value.indexOf '::'
      if i != -1
        key_id = Number.parseInt value[0... i]
        filename = value[(i + 2) ..]
      else
        filename = value
      one = {
        key_id
        filename
      }
      switch key_iv
        when 'key'
          key_file_list.push one
        when 'iv'
          iv_file_list.push one
    else
      key_id = 0  # default key_id
      i = value.indexOf ':'
      if i != -1
        key_id = Number.parseInt value[0... i]
        value = value[(i + 1) ..]
      value = Buffer.from value, format
      switch key_iv
        when 'key'
          config.m3u8_key value, key_id
        when 'iv'
          config.m3u8_iv value, key_id

  o = {}
  while rest.length > 0
    one = _next()
    switch one
      when '--help', '--version'
        o.type = one
      when '-o', '--output'
        config.output_dir _next()

      when '-T', '--thread'
        t = Number.parseInt _next()
        config.dl_thread t
        if t < 1
          throw new Error "bad thread num #{t}"
      when '--auto-remove'
        config.auto_remove true
      when '--exit-on-flag'
        config.exit_on_flag true
      when '-H', '--header'
        _set_header _next()

      when '--proxy-http'
        p = _split_ip_port _next()
        p.type = 'http'
        config.proxy p
      when '--proxy-socks5'
        p = _split_ip_port _next()
        p.type = 'socks5'
        config.proxy p
      when '--dl-with-curl'
        config.curl_bin _next()

      when '--m3u8-base-url'
        config.m3u8_base_url _next()

      when '--m3u8-key'
        _set_key_iv 'key', 'hex', _next()
      when '--m3u8-iv'
        _set_key_iv 'iv', 'hex', _next()
      when '--m3u8-key-base64'
        _set_key_iv 'key', 'base64', _next()
      when '--m3u8-iv-base64'
        _set_key_iv 'iv', 'base64', _next()
      when '--m3u8-key-file'
        _set_key_iv 'key', 'file', _next()
      when '--m3u8-iv-file'
        _set_key_iv 'iv', 'file', _next()

      else  # default: m3u8
        # warning before set
        if o.m3u8?
          log.w "set M3U8 to #{one}"
        o.m3u8 = one
  if (! o.type?) && (! o.m3u8?)
    throw new Error "empty command line"
  # check set headers
  if Object.keys(headers).length > 0
    log.d "use headers #{util.print_json headers}"
    config.headers headers
  # key/iv files to load
  o.m3u8_key_file = key_file_list
  o.m3u8_iv_file = iv_file_list
  o

_normal = (a) ->
  # load key/iv files (support multi-keys)
  key_list = a.m3u8_key_file
  iv_list = a.m3u8_iv_file
  for i in key_list
    log.d "load KEY (#{i.key_id}) file #{i.filename}"
    config.m3u8_key i.key_id, await async_.read_file_byte(i.filename)
  for i in iv_list
    log.d "load IV (#{i.key_id}) file #{i.filename}"
    config.m3u8_iv i.key_id, await async_.read_file_byte(i.filename)

  # DEBUG output: key/iv set from command line
  key = config.get_all_m3u8_key()
  iv = config.get_all_m3u8_iv()
  flag_debug = false
  o = {}
  if Object.keys(key).length > 0
    o.key = key
    flag_debug = true
  if Object.keys(iv).length > 0
    o.iv = iv
    flag_debug = true
  if flag_debug
    log.d "use KEY #{util.print_json o}"

  await do_dl a.m3u8
  # try to remove lock file
  fd = config.lock_file_fd()
  try  # close file before remove it
    await async_.fs_close fd
  catch e
    # ignore error
  try
    await async_.rm config.LOCK_FILE
  catch e
    # ignore
  # check lock file exist
  if await async_.file_exist config.LOCK_FILE
    log.e "can not remove LOCK file `#{path.resolve config.LOCK_FILE}`"
  # FIX process will not exit
  process.exit 0

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

_start = ->
  try
    await main(process.argv[2..])
  catch e
    # DEBUG
    console.log "ERROR: #{e.stack}"
    #throw e
    process.exit 1
_start()
