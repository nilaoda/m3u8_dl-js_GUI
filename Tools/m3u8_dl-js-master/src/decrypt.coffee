# decrypt.coffee, m3u8_dl-js/src/
crypto = require 'crypto'

log = require './log'


# decrypt m3u8 with `aes-128-cbc` (for '#EXT-X-KEY:METHOD=AES-128,')
create_decrypt_stream = (key, iv) ->
  # if key is str, process as hex
  if typeof key is 'string'
    key = Buffer.from key, 'hex'
  # else: key should be a buffer
  if ! key instanceof Buffer
    throw new Error('key is not Buffer')
  # check key length: 128bit (8 Byte)
  if key.length != (128 / 8)
    log.w "decrypt: key length (#{key.length * 8}) is not 128bit !"
  # if iv in number, make it as buffer
  if typeof iv is 'number'
    i = iv
    # iv is 128bit, as BE
    iv_size = 128 / 8
    iv = Buffer.alloc(iv_size)
    iv.writeUInt32BE i, iv_size - 4
  # TODO more check for iv

  crypto.createDecipheriv 'aes-128-cbc', key, iv

module.exports = {
  create_decrypt_stream
}
