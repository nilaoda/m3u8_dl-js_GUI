#!/usr/bin/env python3.6
# ppp_unpacker.py, m3u8_dl-js_GUI/pre_processor/ppp_unpacker/
# unpack pre-processor's `.zip.ppp` file, and check `pre_processor.meta.json`
#
# Usage:
#     --to TMP_DIR ZIP_FILE
# or
#     --check PP_DIR
#
import os, sys
import zipfile
import shutil
import json


PP_VERSION = '0.1.0'  # pre_processor specification version
META_FILE = 'pre_processor.meta.json'

# check `pre_processor.meta.json`, return True if error
def _check_meta(raw_text):
    try:
        meta = json.loads(raw_text)
    except Exception:
        print('ERROR: meta: bad json format !')
        raise
    if not isinstance(meta, dict):
        print('ERROR: meta: bad json format !')
        return True
    must_keys = [
        'pp_version',
        'id', 'name', 'version',
        'type', 'main',
    ]
    optional_keys = [
        'license', 'author', 'copyright', 'home',
    ]
    # check pp_version
    if meta.get('pp_version', None) != PP_VERSION:
        print('ERROR: meta: pp_version != ' + PP_VERSION)
        return 1
    # check must keys
    bad_key = False
    for i in must_keys:
        k = meta.get(i, None)
        if not isinstance(k, str):
            print('ERROR: meta: bad key `' + i + '`')
            bad_key = True
        elif k.strip() == '':
            print('ERROR: meta: empty key `' + i + '`')
            bad_key = True
    # check optional keys
    for i in optional_keys:
        if (i in meta) and (not isinstance(meta[i], str)):
            print('ERROR: meta: bad key `' + i + '`')
            bad_key = True
    if bad_key:
        return 1
    # check unknow keys
    allow_keys = must_keys + optional_keys
    for i in meta.keys():
        if (not i in allow_keys) and (not i.startswith('_')):
            print('WARNING: unknow key `' + i + '`')
    # check done
    return False


# to process `.zip.ppp` file
class Ppp(object):
    def __init__(self, zip_file):
        # open the zip file
        self._z = zipfile.ZipFile(zip_file)

    def get_file_list(self):
        return self._z.namelist()

    def get_zip_root(self):
        for i in self.get_file_list():
            p = os.path.split(i)
            if p[0] == '':
                return p[1]
            elif p[1] == '':
                return p[0]
        return None

    def find_meta(self):
        l = self.get_file_list()
        if META_FILE in l:
            return META_FILE
        # find meta in root
        root = self.get_zip_root()
        if root == None:
            return None
        for i in l:
            p = os.path.split(i)
            if (p[0] == root) and (p[1] == META_FILE):
                return i
        return None

    def extract_meta(self):
        meta_file = self.find_meta()
        if meta_file == None:
            return None
        else:
            with self._z.open(meta_file, 'r') as f:
                return f.read()

    def extract(self, pp_dir):
        tmp_dir = pp_dir + '.tmp'
        self._z.extractall(tmp_dir)
        # move files to pp_dir
        root = self.get_zip_root()
        if root == None:
            os.rename(tmp_dir, pp_dir)
        else:
            os.rename(os.path.join(tmp_dir, root), pp_dir)
            # remove tmp_dir
            shutil.rmtree(tmp_dir)

def _to(pp_dir, zip_file):
    # DEBUG
    print('D: open ' + zip_file)
    try:
        p = Ppp(zip_file)
    except Exception:
        print('ERROR: can not open zip file ' + zip_file)
        raise
    # find meta file
    meta_file = p.find_meta()
    if meta_file == None:
        print('ERROR: can not find meta file `' + META_FILE + '` !')
        return 1
    # check meta file
    meta_text = p.extract_meta().decode('utf-8')
    print('D: check meta file ' + meta_file)
    if _check_meta(meta_text):
        print('ERROR: bad meta file !')
        return 1
    # do extract
    print('D: extract files to ' + pp_dir)
    p.extract(pp_dir)

    return 0  # done

def _check(pp_dir):
    meta_file = os.path.join(pp_dir, META_FILE)
    try:
        with open(meta_file, 'rb') as f:
            meta_text = f.read().decode('utf-8')
    except Exception:
        print('ERROR: can not open meta file ' + meta_file)
        raise
    # check meta file
    if _check_meta(meta_text):
        print('ERROR: bad meta file !')
        return 1
    return 0  # done


def _p_arg(args):
    _a = {
        'rest': args
    }
    o = {}
    def _next():
        o = _a['rest'][0]
        _a['rest'] = _a['rest'][1:]
        return o
    while len(_a['rest']) > 0:
        one = _next()
        if one == '--to':
            o['type'] = 'to'
            o['pp_dir'] = _next()
        elif one == '--check':
            o['type'] = 'check'
            o['pp_dir'] = _next()
        else:  # default: ZIP_FILE
            o['zip_file'] = one
    return o

def main(args):
    a = _p_arg(args)
    if not 'type' in a:
        print('ERROR: bad command line. ')
        return 1
    if a['type'] == 'to':
        return _to(a['pp_dir'], a['zip_file'])
    else:
        return _check(a['pp_dir'])

if __name__ == '__main__':
    exit(main(sys.argv[1:]))

# end ppp_unpacker.py
