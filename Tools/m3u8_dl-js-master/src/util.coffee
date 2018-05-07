# util.coffee, m3u8_dl-js/src/
path = require 'path'
url = require 'url'

async_ = require './async'
log = require './log'
config = require './config'


last_update = ->
  new Date().toISOString()

# pretty-print JSON text
print_json = (data) ->
  JSON.stringify data, '', '    '


WRITE_REPLACE_FILE_SUFFIX = '.tmp'
# atomic write-replace for a file
write_file = (file_path, text) ->
  tmp_file = file_path + WRITE_REPLACE_FILE_SUFFIX
  await async_.write_file tmp_file, text
  await async_.mv tmp_file, file_path

create_lock_file = (file_path) ->
  try
    return await async_.fs_open file_path, 'wx'
  catch e
    log.e "can not create LOCK file #{file_path} "
    throw e

get_base_url = (full_url) ->
  o = url.parse full_url
  # clear values
  o.hash = null
  o.search = null
  o.query = null
  o.path = null
  o.href = null

  o.pathname = path.posix.dirname o.pathname
  url.format(o)

p_bad_command_line = ->
  log.e 'bad command line, please try `--help` '

p_version = ->
  # print version
  console.log config.P_VERSION

check_change_cwd = (create_dir)->
  to = config.output_dir()
  if ! to?
    return
  to_path = path.resolve to
  # if output dir not exist, try to create it
  if (! await async_.file_exist(to)) && create_dir
    log.d "create dir #{to_path}"
    await async_.mkdir to
  process.chdir to
  cwd = process.cwd()
  if path.resolve(cwd) != to_path
    log.w "can not change current directory to `#{to_path}`, current directory is `#{cwd}`"

check_merge_base_url = (raw_url) ->
  o = url.parse raw_url
  if ! o.protocol?
    base = config.m3u8_base_url()
    if ! base?
      log.e "no base URL for #{raw_url}"
      throw new Error "no base URL"
    # merge base url
    if ! base.endsWith('/')
      base += '/'
    o = new url.URL raw_url, base
    url.format o
  else
    raw_url

# run command and check exit_code is 0  (else will throw Error)
run_check = (cmd) ->
  exit_code = await async_.run_cmd cmd
  if exit_code != 0
    throw new Error "run command FAILED  (exit_code = #{exit_code})"


module.exports = {
  last_update
  print_json

  WRITE_REPLACE_FILE_SUFFIX
  write_file  # async

  create_lock_file  # async
  check_change_cwd  # async
  run_check  # async

  get_base_url
  p_bad_command_line
  p_version

  check_merge_base_url
}
