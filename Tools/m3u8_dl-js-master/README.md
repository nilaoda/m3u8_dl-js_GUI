# m3u8_dl-js
A simple `m3u8` downloader in `node.js`.


## Build from source

+ **1**. Install `node.js` (<https://nodejs.org/en/download/current/>)

+ **2**.

  ```
  $ npm install
  $ node ./node_modules/.bin/coffee -o dist/ src/
  ```

**Run**

```
$ node dist/m3u8_dl.js --version
m3u8_dl-js version 0.2.0 test20170606 2238
```


## Usage

+ **m3u8_dl**

  ```
  $ node dist/m3u8_dl.js --help
  m3u8_dl-js [OPTIONS] M3U8
  Usage:
    -o, --output DIR  Download files to this Directory

    -T, --thread NUM            Set number of download thread (default: 1)
        --auto-remove           Remove raw file after decrypt success
        --exit-on-flag          Exit when FLAG file exist
    -H, --header NAME:VALUE     Set http header (can use more than once)
        --proxy-http IP:PORT    Set http proxy
        --proxy-socks5 IP:PORT  Set socks5 proxy
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
  $
  ```

+ **auto_retry**

  ```
  $ node dist/auto_retry.js --help
  auto_retry [OPTIONS] M3U8 -- OPTIONS_FOR_M3U8_DL
  Usage:
    -o, --output DIR  Download files to this Directory

        --retry NUM     Retry times (default: 3)
        --use-raw-m3u8  Use `raw.m3u8` file for retry
        --sleep SEC     Sleep seconds before next retry (default: 1)

        --remove-part-files  Remove all `.part` files before run m3u8_dl-js

        --version  Show version of this program
        --help     Show this help text
  More information online <https://github.com/sceext2/m3u8_dl-js>
  $
  ```

+ **show_dl_speed**

  ```
  $ node dist/show_dl_speed.js --help
  show_dl_speed [OPTIONS] [DIR]
  Usage:

    --put-exit-flag    Enable retry function (put flag file)

    --retry-after SEC  Retry after this seconds (default: 10)
    --retry-hide SEC   Hide retry debug info in this seconds (default: 5)
    --init-wait SEC    Wait seconds for init_wait mode (default: 20)

    --version  Show version of this program
    --help     Show this help text
  More information online <https://github.com/sceext2/m3u8_dl-js>
  $
  ```


## LICENSE

`GNU GPL v3+`

<!-- end README.md -->
