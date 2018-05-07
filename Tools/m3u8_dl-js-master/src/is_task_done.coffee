# is_task_done.coffee, m3u8_dl-js/src/
#
# Usage:
#     is_task_done DIR
#
# exit 0 if task done
#
path = require 'path'

util = require './util'
log = require './log'
config = require './config'
dl_speed = require './dl_speed'


main = (argv) ->
  config.output_dir argv[0]
  await util.check_change_cwd()
  log.d "load meta file #{path.resolve(config.META_FILE)}"

  await dl_speed.load_meta_file()
  exit_flag = await dl_speed.update()
  clip = dl_speed.get_clip_count()
  if exit_flag
    log.p "[ OK ] task done (#{clip.done}/#{clip.all})"
    process.exit 0  # task done
  else
    log.e "task NOT done (#{clip.done}/#{clip.all})"
    process.exit 1  # task NOT done

_start = ->
  try
    await main(process.argv[2..])
  catch e
    # DEBUG
    console.log "ERROR: #{e.stack}"
    #throw e
    process.exit 1  # task NOT done
_start()
