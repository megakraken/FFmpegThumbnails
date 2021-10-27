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

static int get_video_stream_index(AVFormatContext* ic);
static int read_cb(void* opaque, uint8_t* buf, int bufSize);
static int64_t seek_cb(void* opaque, int64_t offset, int whence);

HRESULT GetThumbnail(IN IStream* stream, int cx, OUT HBITMAP* thumbnail) {
    // TODO: Implement
    HRESULT ret = E_FAIL;
    int ioBufferSize = 32768;
    AVIOContext* avioCtx = NULL;
    AVFormatContext* fmtCtx = NULL;
    AVCodecContext* codecCtx = NULL;
    AVCodecParameters* codecParams = NULL;
    AVCodec* codec = NULL;
    AVFrame* frame = NULL;
    int streamIdx = -1;
    unsigned char* ioBuffer = av_malloc(ioBufferSize + AV_INPUT_BUFFER_PADDING_SIZE);
    if (ioBuffer == NULL) {
        ret = E_OUTOFMEMORY;
        goto end;
    }
    if ((avioCtx = avio_alloc_context( ioBuffer, ioBufferSize, 0, stream, &read_cb,
        NULL, &seek_cb)) == NULL) {
        ret = E_OUTOFMEMORY;
        goto end;
    }
    if((fmtCtx = avformat_alloc_context()) == NULL) {
        ret = E_OUTOFMEMORY;
        goto end;
    }
    fmtCtx->pb = avioCtx;
    if ((ret = avformat_open_input(&fmtCtx, "dummy", NULL, NULL)) != 0)
        goto end;
    if ((ret = avformat_find_stream_info(fmtCtx, NULL)) < 0)
        goto end;
    // Find the first video stream
    if ((ret = get_video_stream_index(fmtCtx)) < 0)
        goto end;
    streamIdx = ret;
    codecParams = fmtCtx->streams[streamIdx]->codecpar;
    if ((codec = avcodec_find_decoder(codecParams->codec_id)) == NULL) {
        ret = AVERROR_DECODER_NOT_FOUND;
        goto end;
    }
    if ((codecCtx = avcodec_alloc_context3(codec)) == NULL) {
        ret = E_OUTOFMEMORY;
        goto end;
    }
    if ((ret = avcodec_parameters_to_context(codecCtx, codecParams)) < 0)
        goto end;
    if ((ret = avcodec_open2(codecCtx, codec, NULL)) < 0)
        goto end;
    if ((frame = av_frame_alloc()) == NULL) {
        ret = E_OUTOFMEMORY;
        goto end;
    }
    *thumbnail = (HBITMAP)LoadImage(NULL, L"test.bmp",
        IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE);
end:
    if (codecCtx)
        avcodec_free_context(&codecCtx);
    if (fmtCtx)
        avformat_close_input(&fmtCtx);
    if (avioCtx)
        avio_context_free(&avioCtx);
    return ret;
}

static int get_video_stream_index(AVFormatContext* ic) {
    for (int i = 0; i < ic->nb_streams; i++) {
        if (ic->streams[i]->codecpar->codec_type == AVMEDIA_TYPE_VIDEO)
            return i;
    }
    return AVERROR_STREAM_NOT_FOUND;
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