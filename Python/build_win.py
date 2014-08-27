# Papagayo, a lip-sync tool for use with Lost Marble's Moho
# Copyright (C) 2005 Mike Clifton
# Contact information at http://www.lostmarble.com
#
# This program is free software; you can redistribute it and/or
# modify it under the terms of the GNU General Public License
# as published by the Free Software Foundation; either version 2
# of the License, or (at your option) any later version.
#
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
#
# You should have received a copy of the GNU General Public License
# along with this program; if not, write to the Free Software
# Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

# setup.py
# To run: python setup_win.py py2exe
# To display options: python setup_win.py py2exe --help
# To run in GUI mode: setup(windows=["app.py"])
# To run in command-line mode: setup(console=["app.py"])
from distutils.core import setup
import os
import glob
#import glob
import py2exe

def find_data_files(source,target,patterns):
	"""Locates the specified data-files and returns the matches
        in a data_files compatible format.
    
        source is the root of the source data tree.
            Use '' or '.' for current directory.
       target is the root of the target data tree.
           Use '' or '.' for the distribution directory.
       patterns is a sequence of glob-patterns for the
           files you want to copy.
	"""
	if glob.has_magic(source) or glob.has_magic(target):
		raise ValueError("Magic not allowed in src, target")
	ret = {}
	for pattern in patterns:
		pattern = os.path.join(source,pattern)
		for filename in glob.glob(pattern):
			if os.path.isfile(filename):
				targetpath = os.path.join(target,os.path.relpath(filename,source))
				path = os.path.dirname(targetpath)
				ret.setdefault(path,[]).append(filename)

	return sorted(ret.items())

dll_excludes = ['MSVCP90.dll']	
	
setup(
	windows = [{
		"script": "unigayo.py",
		"icon_resources": [(1, "papagayo.ico")],
		}],
	options = {"py2exe": {
		"compressed": 1,
		"optimize": 2,
		"packages": ["encodings"],
		"dll_excludes": dll_excludes,
	}},
	name = "UniGayo",
	version = "1.0",
	data_files = find_data_files("rsrc", "rsrc", ['*.*', 'languages/*/*', 'mouths/*/*',]),
		
)
