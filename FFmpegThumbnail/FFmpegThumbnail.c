/*
 * Contains the FFmpeg code to generate thumbnails for video files.
 *
 * Provides a single method that is called from the IThumbnailProvider to generate
 * and return a thumbnail as a HBITMAP for a given IStream.
 */
#ifndef _CRT_SECURE_NO_WARNINGS
#define _CRT_SECURE_NO_WARNINGS
#endif
#ifndef COBJMACROS
#define COBJMACROS
#endif

#pragma comment(lib, "avcodec.lib")
#pragma comment(lib, "avformat.lib")
#pragma comment(lib, "avutil.lib")
#pragma comment(lib, "swscale.lib")

#include <libavcodec/avcodec.h>
#include <libavformat/avformat.h>
#include <libavcodec/defs.h>
#include <libswscale/swscale.h>
#include <stdio.h>
#include <stdarg.h>
#include <stdlib.h>
#include <string.h>
#include <inttypes.h>
#include <objidl.h>

HRESULT GetThumbnail(IN IStream* stream, int cx, OUT HBITMAP* thumbnail) {
    // TODO: Implement
    return S_FALSE;
}