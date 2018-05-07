# play_server.coffee, m3u8_dl-js/o/play_server/
path = require 'path'
http = require 'http'

express = require 'express'

async_ = require '../async'
util = require '../util'
config = require '../config'

make_m3u8 = require './make_m3u8'


MIME_TYPE = {
  m3u8: 'application/x-mpegURL'
  ts: 'video/MP2T'
}

_etc = {
  meta: null  # loaded meta data

  port: 8800  # port to listen
}


_serve_m3u8 = (req, res) ->
  u = req.path
  if u is '/raw.m3u8'
    # check and load meta data
    if await async_.file_exist config.META_FILE
      text = await async_.read_file config.META_FILE
      _etc.meta = JSON.parse text
      # TODO check meta.p_version ?
      m3u8 = make_m3u8 _etc.meta
      data = Buffer.from m3u8
      res.status 200
      res.set 'Content-Type', MIME_TYPE.m3u8
      res.set 'Content-Length', data.length
      res.send data
      res.end()
    else
      # DEBUG
      console.log "DEBUG: meta file `#{config.META_FILE}` not exist !"
      _res_code res, 404
  else  # `XXXX.ts` file
    if ! _etc.meta?
      _res_code res, 404
    else  # check is one clip name
      for c in _etc.meta.m3u8_info.clip
        if u is ('/' + c.name.ts)
          # check file exist
          if await async_.file_exist c.name.ts
            # send this clip
            filepath = path.resolve c.name.ts

            res.sendFile filepath, {
              headers: {
                'Content-Type': MIME_TYPE.ts
              }
            }
          else
            _res_code res, 404
          return
      _res_code res, 404

_res_code = (res, code) ->
  text = Buffer.from "HTTP #{code} #{http.STATUS_CODES[code]}\n"
  res.status code
  res.set 'Content-Type', 'text/plain'
  res.set 'Content-Length', text.length
  res.send text
  res.end()

_init_server = ->
  app = express()
  serve_m3u8 = express()
  # /m3u8_dl-js/play_m3u8/ -> _serve_m3u8
  serve_m3u8.all '*', (req, res) ->
    # check method
    if req.method != 'GET'
      _res_code res, 405
      return
    _serve_m3u8(req, res).catch( (err) ->
      # FIXME
      console.log err.stack

      _res_code res, 500
    ).then () ->
      # TODO
  app.use '/m3u8_dl-js/play_m3u8/', serve_m3u8

  # TODO static page ?
  root = express()
  root.all '*', (req, res) ->
    # check method
    if req.method != 'GET'
      _res_code res, 405
      return
    u = req.path  # request url
    switch u
      #when '/'
      # / -> res 404
      # TODO for /
      # /m3u8 -> 302: /m3u8_dl-js/play_m3u8/raw.m3u8
      when '/m3u8'
        to = '/m3u8_dl-js/play_m3u8/raw.m3u8'
        res.redirect 302, to
        res.end()
      else  # res 404
        _res_code res, 404
  app.use '/', root
  # init done
  app

_normal = (a) ->
  if a.port?
    _etc.port = a.port
  # change working directory
  if a.root_dir?
    process.chdir a.root_dir
  # DEBUG
  console.log "DEBUG: working directory: #{process.cwd()}"

  app = _init_server()
  app.listen _etc.port, '127.0.0.1', () ->
    play_url = "http://127.0.0.1:#{_etc.port}/m3u8"
    if a.name?
      play_url += "?name=#{a.name}"
    # DEBUG
    console.log "Play at #{play_url}"

_p_help = ->
  console.log '''
  play_server [OPTIONS] [DIR]
  Usage:
    --port PORT  Port to listen (http server)
    --name NAME  Add a comment for the m3u8 to play

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

  o = {}
  while rest.length > 0
    one = _next()
    switch one
      when '--help', '--version'
        o.type = one
      when '--port'
        o.port = Number.parseInt _next()
      when '--name'
        o.name = _next()
      else  # default: DIR
        o.root_dir = one
  o

_start = ->
  a = _p_arg process.argv[2..]
  switch a.type
    when '--help'
      _p_help()
    when '--version'
      util.p_version()
    else
      _normal a

_start()
