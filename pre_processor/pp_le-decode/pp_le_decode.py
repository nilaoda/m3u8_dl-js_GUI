#!/usr/bin/env python3.6
# pp_le_decode.py, m3u8_dl-js_GUI/pre_processor/pp_le-decode/
#
# upstream: <https://github.com/zhangn1985/ykdl>
#
import sys

def decode(data):
    version = data[0:5]
    if version.lower() == b'vc_01':
        loc2 = data[5:]
        length = len(loc2)
        loc4 = [0] * (2 * length)
        for i in range(length):
            loc4[2 * i] = int(loc2[i]) >> 4
            loc4[2 * i + 1] = int(loc2[i]) & 15
        loc6 = loc4[len(loc4) - 11 :] + loc4[:len(loc4) - 11]
        loc7 = [0] * length
        for i in range(length):
            loc7[i] = (loc6[2 * i] << 4) + loc6[2 * i + 1]
        return ('').join([chr(i) for i in loc7])
    else:
        return data

def main(arg):
    OUT_FILE, RAW_FILE = arg[0], arg[1]

    with open(RAW_FILE, 'rb') as f:
        data = f.read()
    text = decode(data)

    with open(OUT_FILE + '.m3u8', 'wt') as f:
        f.write(text)

if __name__ == '__main__':
    exit(main(sys.argv[1:]))
# end pp_le_decode.py
