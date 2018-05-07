# log.coffee, m3u8_dl-js/src/

_PREFIX = 'm3u8_dl'


# TODO print to stderr ?
_p = (text) ->
  o = _PREFIX + text
  console.log o


# exports
d = (text) ->
  _p '.D: ' + text
e = (text) ->
  _p '.ERROR: ' + text
w = (text) ->
  _p '.WARNING: ' + text
p = (text) ->
  _p ': ' + text


module.exports = {
  d  # DEBUG
  e  # ERROR
  w  # WARNING

  p  # normal print
}
