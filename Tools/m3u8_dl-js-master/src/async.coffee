# async.coffee, m3u8_dl-js/src/
# TODO use node v8.0 util.promisify ?

fs = require 'fs'
child_process = require 'child_process'


read_file = (file_path) ->
  new Promise (resolve, reject) ->
    fs.readFile file_path, 'utf8', (err, data) ->
      if err
        reject err
      else
        resolve data

read_file_byte = (filename) ->
  new Promise (resolve, reject) ->
    fs.readFile filename, (err, data) ->
      if err
        reject err
      else
        resolve data

write_file = (file_path, text) ->
  new Promise (resolve, reject) ->
    fs.writeFile file_path, text, 'utf8', (err) ->
      if err
        reject err
      else
        resolve()

# move file
mv = (from, to) ->
  new Promise (resolve, reject) ->
    fs.rename from, to, (err) ->
      if err
        reject err
      else
        resolve()

# check if file exist
file_exist = (file_path) ->
  new Promise (resolve, reject) ->
    fs.access file_path, fs.constants.R_OK, (err) ->
      if err
        resolve false
      else
        resolve true

# if file not exist, return null
get_file_size = (file_path) ->
  new Promise (resolve, reject) ->
    fs.stat file_path, (err, stats) ->
      if err  # never reject
        resolve null
      else
        resolve stats.size

list_dir = (file_path) ->
  new Promise (resolve, reject) ->
    fs.readdir file_path, (err, file_list) ->
      if err
        reject err
      else
        resolve file_list

mkdir = (file_path) ->
  new Promise (resolve, reject) ->
    fs.mkdir file_path, (err) ->
      if err
        reject err
      else
        resolve()

# for file-lock
fs_open = (file_path, flags) ->
  new Promise (resolve, reject) ->
    fs.open file_path, flags, (err, fd) ->
      if err
        reject err
      else
        resolve fd

fs_close = (fd) ->
  new Promise (resolve, reject) ->
    fs.close fd, (err) ->
      if err
        reject err
      else
        resolve()


# remove file
rm = (file_path) ->
  new Promise (resolve, reject) ->
    fs.unlink file_path, (err) ->
      if err
        reject err
      else
        resolve()

# sleep: setTimeout
sleep = (time_ms) ->
  new Promise (resolve, reject) ->
    _callback = ->
      resolve()  # never reject
    setTimeout _callback, time_ms

# run shell command, pipe stdin -> stdin, stdout -> stdout, stderr -> stderr, return exit_code
run_cmd = (args) ->
  new Promise (resolve, reject) ->
    cmd = args[0]
    rest = args[1..]
    # DEBUG
    console.log "  run -> #{args.join(' ')}"
    p = child_process.spawn cmd, rest, {
      stdio: 'inherit'
    }
    p.on 'error', (err) ->
      reject err
    p.on 'exit', (exit_code) ->
      resolve exit_code


module.exports = {
  read_file  # async
  read_file_byte  # async
  write_file  # async

  mv  # async
  rm  # async
  file_exist  # async
  get_file_size  # async
  list_dir  # async

  mkdir  # async
  fs_open  # async
  fs_close  # async

  sleep  # async
  run_cmd  # async
}
