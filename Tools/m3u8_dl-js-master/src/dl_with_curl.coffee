# dl_with_curl.coffee, m3u8_dl-js/src/

util = require './util'
log = require './log'
config = require './config'

dl_with_curl = (file_url, filename) ->
  # make curl args
  arg = ['-Ss', '-L']
  # support proxy
  proxy = config.proxy()
  if proxy?
    switch proxy.type
      when 'http'
        arg.push '--proxy'
        arg.push "http://#{proxy.hostname}:#{proxy.port}"
      when 'socks5'
        arg.push '--proxy'
        arg.push "socks5://#{proxy.hostname}:#{proxy.port}"
      else
        throw new Error "unknow proxy.type #{proxy.type}"
  # add custom headers
  ch = config.headers()
  if ch?
    for i of ch
      arg.push '-H'
      arg.push "#{i}: #{ch[i]}"
  # normal options
  CURL_BIN = config.curl_bin()
  args = [CURL_BIN].concat(arg).concat ['-o', filename, file_url]
  # TODO remove `*.part` file before call curl ?
  # call curl
  await util.run_check args

module.exports = dl_with_curl  # async
