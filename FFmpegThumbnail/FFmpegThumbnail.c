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

static int read_cb(void* opaque, uint8_t* buf, int bufSize);
static int64_t seek_cb(void* opaque, int64_t offset, int whence);

HRESULT GetThumbnail(IN IStream* stream, int cx, OUT HBITMAP* thumbnail) {
    // TODO: Implement
    HRESULT ret = E_FAIL;
    int ioBufferSize = 32768;
    AVIOContext *avioContext = NULL;
    AVFormatContext *pFormatContext = NULL;
    unsigned char* ioBuffer = av_malloc(ioBufferSize + AV_INPUT_BUFFER_PADDING_SIZE);
    if (ioBuffer == NULL) {
        ret = E_OUTOFMEMORY;
        goto end;
    }
    if ((avioContext = avio_alloc_context( ioBuffer, ioBufferSize, 0, stream, &read_cb,
        NULL, &seek_cb)) == NULL) {
        ret = E_OUTOFMEMORY;
        goto end;
    }
    if((pFormatContext = avformat_alloc_context()) == NULL) {
        ret = E_OUTOFMEMORY;
        goto end;
    }

    *thumbnail = (HBITMAP)LoadImage(NULL, L"test.bmp",
        IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE);
end:
    if (pFormatContext)
        avformat_close_input(&pFormatContext);
    if (avioContext)
        avio_context_free(&avioContext);
    return ret;
}

static int read_cb(void* opaque, uint8_t* buf, int bufSize) {
    IStream* stream = (IStream*)opaque;
    ULONG numBytes;
    IStream_Read(stream, buf, bufSize, &numBytes);
    if (numBytes == 0)
        return AVERROR_EOF;
    return numBytes;
}

static int64_t seek_cb(void* opaque, int64_t offset, int whence) {
    IStream* stream = (IStream*)opaque;
    if (whence & AVSEEK_SIZE) {
        STATSTG stg;
        if (IStream_Stat(stream, &stg, STATFLAG_DEFAULT) != S_OK)
            return -1;
        return stg.cbSize.QuadPart;
    } else {
        ULARGE_INTEGER newPosition;
        LARGE_INTEGER _offset;
        _offset.QuadPart = offset;
        if (IStream_Seek(stream, _offset, whence, &newPosition) != S_OK)
            return -1;
        return newPosition.QuadPart;
    }
}