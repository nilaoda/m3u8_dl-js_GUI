# thread_pool.coffee, m3u8_dl-js/src/

# create a new thread pool
thread_pool = (pool_size) ->
  run = (todo_list, worker) ->
    o = []  # output (return) result
    current_run_thread_count = 0  # current running tasks count
    current_todo_task_id = 0  # this task is waiting todo

    new Promise (resolve, reject) ->
      # check and run a new thread
      check_run = ->
        if current_todo_task_id < todo_list.length
          # check start a new thread
          if current_run_thread_count < pool_size
            current_task_id = current_todo_task_id
            current_todo_task_id += 1
            current_todo = todo_list[current_task_id]

            # start worker
            worker(current_todo).catch( (err) ->
              # save error in result
              o[current_task_id] = err
              # end this thread
              current_run_thread_count -= 1
              # check after thread end
              check_run()
            ).then (result) ->
              # save result
              o[current_task_id] = result
              # end this thread
              current_run_thread_count -= 1
              check_run()
            # add current thread count
            current_run_thread_count += 1
            # check after start a thread
            check_run()
          # else: waiting more threads to end
        # else: waiting tasks to finish
        else if current_run_thread_count < 1
          resolve o  # done
        # else: still waiting
      check_run()
  o = {
    run  # async
  }

module.exports = thread_pool
