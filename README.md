[![Build Status](https://travis-ci.org/bwrsandman/Bless.svg?branch=master)](https://travis-ci.org/bwrsandman/Bless)

Bless - Gtk# Hex Editor v0.6.0
==============================
Copyright (c) 2004-2008, Alexandros Frantzis

Thank you for using ( or at least trying out :) ) Bless!

### Contents

1. [Description](#1-description)
2. [Project Web Site and contact info](#2-project-web-site-and-contact-info)
3. [Requirements](#3-requirements)
4. [Installation](#4-installation)
5. [Running](#5-running)
6. [Documentation](#6-documentation)
7. [Known Issues](#7-known-issues)


## 1. Description

Bless is a binary (hex) editor, a program that enables you to edit files as
a sequence of bytes. It is written in C# and uses the Gtk# bindings for the 
GTK+ toolkit.

Bless is distributed under the terms of the GNU General Public License (GPL). 
See the file COPYING for more information.

### Main Features

  * Efficient editing of large data files. 
  * Raw disk editing.
  * Multilevel undo - redo operations.
  * Customizable data views.
  * Fast data rendering on screen.
  * Multiple Tabs.
  * Fast Find and Replace operations.
  * Conversion Table.
  * Advanced Copy/Paste capabilities.
  * Multi-threaded search and save operations.
  * Export to text and html (others with plugins).
  * Extensibility with Plugins.
  
### Planned Features

  * Scripting language for binary file manipulation.


## 2. Project Web Site and contact info


More information, bug reports and the latest releases can be found at: 
  http://home.gna.org/bless

The original author can be contacted at: alf82 [at] freemail [dot] gr.

## 3. Requirements

The main target platform for bless is GNU/Linux. However, all the libraries it
uses are cross-platform, so bless should be able to run without problems 
on all the major platforms (GNU/Linux, *BSD, Solaris, Win32).

To build and run the current version of bless you need:
    * GTK+ >= 2.8.x (Included in all modern GNU/Linux distributions, http://www.gtk.org)
    * mono/.NET runtime and C# compiler >= 1.1.14 (http://www.mono-project.com)
    * Gtk# bindings >= 2.8 for GTK+ (http://gtk-sharp.sourceforge.net)
    * pkg-config (Included in all modern GNU/Linux distributions)
    
Development is done using the latest stable versions of the above libraries. 
Although using an older version may be OK, there is no guarantee that there 
will not be problems.


## 4. Installation

```
checkout, nuget, xbuild, install
```

### Step 1: Checkout the package or untar

* For the git source:
    ```
    git clone https://github.com/bwrsandman/Bless.git
    ```

* For a tar.gz package use:
    ```
    tar -xzvf bless-a.b.c.tar.gz
    ```

* For a tar.bz2 package use:
    ```
    tar -xjvf bless-a.b.c.tar.gz
    ```

### Step 2: Get dependencies
Enter the directory created in the previous step (bless-a.b.c) and type:
```
nuget restore Bless.sln
```

This command will download all dependencies of the project.

### Step 3: Build the program

Type:

```
xbuild /p:Configuration=Release Bless.sln
```

This will create Bless.exe and the necessary library files in the `src/bin/`.

You can also run: 

```
mono /usr/lib/mono/4.5/nunit-console.exe tests/bin/Release/tests.dll
```

This will perform some tests on various Bless components.

### Step 4: Install the program (optional)

TODO: Not impemented

### 5. Running

If you chose to install the program, just type 'bless'. In any case you can  
run the program by typing `mono Bless.exe` in the 'bless-a.b.c/src/bin' directory.
Enjoy!


### 6. Documentation

The doc/ directory contains documentation directed both at the user and at the 
developer who wants to explore Bless. The doc/user/ subdirectory contains 
information about using bless whereas doc/developer/ contains developer 
information (bless api etc). 
Note: The developer documentation is almost non-existant.


### 7. Known Issues

* To be able to save a file under the same name (File->Save command) you need
  to have (temporarily) enough disk space to hold both the original and the
  new file. This happens because the new file is created in the /tmp 
  directory and then moved to its proper position. For example if you have a 
  20MB file and edit it so that it becomes 21MB and the new file is supposed to
  be saved in the same storage device as /tmp, you need to have 20+21=41MB free
  space in that storage device to be able to save it. After a successful save, 
  the original file is deleted, in this case freeing 20MB.
  Although this can be a problem (when there is not enough disk space), it 
  can also be seen as a safety measure in case something goes wrong when saving.

  A notable exception to the above is when the size of the file to be saved has 
  not been changed. In that case the file is saved in-place.
