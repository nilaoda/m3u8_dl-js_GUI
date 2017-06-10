#!/usr/bin/env python3.6
# pp_271_m_ts.py, m3u8_dl-js_GUI/pre_processor/pp_271-m-ts/
#
# upstream: <https://github.com/wwqgtxx/wwqLyParse>
# LICENSE: GNU GPL v3+
#
import sys

import re
import time, hashlib
import urllib.request
import json


def r1(r, text):
    m = re.search(r, text)
    if m:
        return m.group(1)

def dl_json(url):
    print('D: GET ' + url)
    r = urllib.request.urlopen(url)
    return json.load(r)


_BID_TO_FORMAT = {
    19 : {
        'index': 7,
        'name': '4K (h265)',
    },
    18 : {
        'index': 6,
        'name': '1080p (h265)',
    },
    10 : {
        'index': 5,
        'name': '4K',  # h264
    },
    5 : {
        'index': 4,
        'name': '1080p',  # h264
    },

    17 : {
        'index': 3,
        'name': '720p (h265)',
    },
    4 : {
        'index': 2,
        'name': '720p',  # h264
    },

    21 : {
        'index': 1,
        'name': '540p (h265)',
    },
    2 : {
        'index': 0,
        'name': '540p',
    },
    1 : {
        'index': -1,
        'name': '360p',
    },
    96 : {
        'index': -2,
        'name': '210p',
    },
}


class Mts(object):

    @staticmethod
    def vms_url(tvid, vid):
        src = '76f90cbd92f94a2e925d83e8ccd22cb7'
        key = 'd5fb4bd9d50c4be6948c97edd7254b0e'

        t = int(time.time() * 1e3)
        sc = hashlib.new('md5', bytes(str(t) + key + vid, 'utf-8')).hexdigest()
        return 'http://cache.m.iqiyi.com/tmts/{0}/{1}/?t={2}&sc={3}&src={4}'.format(tvid, vid, t, sc, src)

    @staticmethod
    def get_info(html, url):
        o = {}
        o['tvid'] = \
            r1(r'#curid=(.+)_', url) or \
            r1(r'tvid=([^&]+)', url) or \
            r1(r'data-player-tvid="([^"]+)"', html)
        o['vid'] = \
            r1(r'#curid=.+_(.*)$', url) or \
            r1(r'vid=([^&]+)', url) or \
            r1(r'data-player-videoid="([^"]+)"', html)
        o['title'] = r1('<title>([^<]+)', html).split('-')[0]
        return o

    @staticmethod
    def parse_vms(vms, tvid):
        OK_CODE = 'A00000'

        if vms['code'] != OK_CODE:
            raise Exception('vms.code = `' + vms['code'] + '` != ' + OK_CODE)
        o = []
        ctl = vms['data']['ctl']
        for bid in ctl['vip']['bids']:
            vid = ctl['configs'][str(bid)]['vid']
            vms_url = Mts.vms_url(tvid, vid)
            print('D: bid = ' + str(bid) + ', vid = ' + str(vid))
            v = dl_json(vms_url)
            if v['code'] != OK_CODE:
                raise Exception('vms.code = `' + v['code'] + '` != ' + OK_CODE)
            m3u8_url = v['data']['m3u']
            o.append({
                'bid': bid,
                'm3u8_url': m3u8_url,
            })
        for s in vms['data']['vidl']:
            o.append({
                'bid': s['vd'],
                'm3u8_url': s['m3u'],
            })
        return o

    @staticmethod
    def output(v):
        o = []
        for i in v:
            one = _BID_TO_FORMAT[i['bid']].copy()
            one['url'] = i['m3u8_url']
            o.append(one)
        o.sort(key = lambda x: x['index'], reverse=True)
        return o

    @staticmethod
    def parse(html, url):
        info = Mts.get_info(html, url)
        print('D: tvid = ' + info['tvid'] + ', vid = ' + info['vid'] + ', title = ' + info['title'])
        vms_url = Mts.vms_url(info['tvid'], info['vid'])
        vms = dl_json(vms_url)

        v = Mts.parse_vms(vms, info['tvid'])
        o = {
            'title': info['title'],
            'v': Mts.output(v),
        }
        return o

def _make_out_file(title, v):
    o = []
    o += ['# ' + title, '']
    for i in v:
        o += ['# ' + i['name'], i['url'], '']
    return ('\n').join(o)

def main(args):
    OUT_FILE, RAW_FILE, URL = sys.argv[1], sys.argv[2], sys.argv[3]

    with open(RAW_FILE, 'rt') as f:
        html = f.read()
    o = Mts.parse(html, URL)
    text = _make_out_file(o['title'], o['v'])

    with open(OUT_FILE + '.format', 'wt') as f:
        f.write(text)

if __name__ == '__main__':
    exit(main(sys.argv[1:]))
# end pp_271_m_ts.py
