#!/usr/bin/env python3.6
# pp_tv-line-me.py, m3u8_dl-js_GUI/pre_processor/pp_tv-line-me/
# m3u8 pre-processor for tv.line.me
#
# <https://github.com/nilaoda/m3u8_dl-js_GUI>
#
import sys

OUT_FILE, RAW_FILE, URL = sys.argv[1], sys.argv[2], sys.argv[3]


query = URL.split('?', 1)[1]
base_url = URL.split('?', 1)[0].rsplit('/', 1)[0]

with open(RAW_FILE, 'rt') as f:
    line = f.readlines()

out = []
for l in line:
    l = l.strip()
    if l.startswith('#') or (l == ''):
        out.append(l)
    else:
        out.append(base_url + '/' + l + '?' + query)
out_text = ('\n').join(out) + '\n'

with open(OUT_FILE + '.m3u8', 'wt') as f:
    f.write(out_text)

# end pp_tv-line-me.py
