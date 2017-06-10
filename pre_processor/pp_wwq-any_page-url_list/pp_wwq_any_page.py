#!/usr/bin/env python3.6
# pp_wwq_any_page.py, m3u8_dl-js_GUI/pre_processor/pp_wwq-any_page-url_list/
#
# upstream: <https://github.com/wwqgtxx/wwqLyParse>
# LICENSE: GNU GPL v3+
#
import sys

import re
from pyquery import PyQuery as pq


def load_page(url):
    d = pq(url=url)
    return d


class AnyPage(object):

    @staticmethod
    def check_a(raw):
        a = pq(raw)
        title = a.attr('title')
        if title == None:
            title = a.text()
        url = a.attr('href')
        # remove bad urls
        if url == None:
            return None
        if (title == None) or (title.strip() == ''):
            return None

        if re.match('^(http|https|ftp)://.+\.(mp4|mkv|ts|avi)', url):
            url = '$-> ' + url
        if not re.match('(^(http|https)://.+\.(shtml|html|mp4|mkv|ts|avi))|(^(http|https)://.+/video/)', url):
            return None
        if re.search('[^\?](list|mall|about|help|shop|map|vip|faq|support|download|copyright|contract|product|tencent|upload|common|index.html|v.qq.com/u/|open.baidu.com|www.iqiyi.com/lib/s_|www.iqiyi.com/dv/|top.iqiyi.com)', url):
            return None
        if re.search('(下载|播 放|播放|投诉|评论|(\d{1,2}:\d{1,2}))', title):
            return None

        return { 'title': title, 'url': url }

    @staticmethod
    def parse(html, url):
        d = pq(html)
        page_title = d('title').text()

        used_urls = {}
        o = []
        for i in d('a'):
            one = AnyPage.check_a(i)
            if one == None:
                continue
            # ignore dup urls
            if one['url'] in used_urls:
                continue
            used_urls[one['url']] = True
            o.append(one)
        return { 'title': page_title, 'list': o }

def _make_out_file(raw):
    o = []
    o += ['# ' + raw['title'], '']
    for i in raw['list']:
        o += ['# ' + i['title'], i['url'], '']
    return ('\n').join(o)

def main(arg):
    OUT_FILE, RAW_FILE, URL = arg[0], arg[1], arg[2]

    with open(RAW_FILE, 'rt') as f:
        html = f.read()
    o = AnyPage.parse(html, URL)
    text = _make_out_file(o)

    with open(OUT_FILE + '.list', 'wt') as f:
        f.write(text)

if __name__ == '__main__':
    exit(main(sys.argv[1:]))
# end pp_wwq_any_page.py
