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
#include <libavutil/imgutils.h>
#include <stdio.h>
#include <stdarg.h>
#include <stdlib.h>
#include <string.h>
#include <inttypes.h>
#include <objidl.h>

static int get_video_stream_index(AVFormatContext *ic);
static int read_cb(void *opaque, uint8_t *buf, int bufSize);
static int64_t seek_cb(void *opaque, int64_t offset, int whence);
static int decode_packet(AVCodecContext *codecCtx, AVPacket *packet, AVFrame *frame,
    AVFrame *frameRGB, struct SwsContext *swsCtx);

/**
 * Gets a thumbnail for the video contained in the given stream.
 * 
 * @param stream  A pointer to an IStream interface that represents the stream
 *                source that contains the video.
 * @param cx      The maximum thumbnail size, in pixels. The returned bitmap should
 *                fit into a square of width and height cx, though it does not need
 *                to be a square image. 
 * @param hbmp    When this function returns, contains a pointer to the thumbnail
 *                image handle. The image is a DIB section and 32 bits per pixel.
 * 
 * @return        If this function succeeds, it returns S_OK. Otherwise, it returns
 *                an HRESULT (or FFMPEG) error code.
 */
HRESULT GetThumbnail(IN IStream *stream, int cx, OUT HBITMAP *hbmp) {
    // TODO: Try to refactor into smaller chunks if possible and makes sense.
    HRESULT ret = E_FAIL;
    int ioBufferSize = 32768;
    AVIOContext *avioCtx = NULL;
    AVFormatContext *fmtCtx = NULL;
    AVCodecContext *codecCtx = NULL;
    AVCodecParameters *codecParams = NULL;
    const AVCodec *codec = NULL;
    AVFrame *frame = NULL, *frameRGB = NULL;
    AVPacket *packet = NULL;
    int streamIdx = -1;
    uint8_t *frameBuf = NULL;
    struct SwsContext *swsCtx = NULL;
    unsigned char *ioBuffer = av_malloc(ioBufferSize + AV_INPUT_BUFFER_PADDING_SIZE);
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
    if ((frameRGB = av_frame_alloc()) == NULL) {
        ret = E_OUTOFMEMORY;
        goto end;
    }
    if ((ret = av_image_get_buffer_size(AV_PIX_FMT_RGB24, codecCtx->width,
        codecCtx->height, 1)) < 0) {
        goto end;
    }
    frameBuf = av_malloc(ret);
    if ((ret = av_image_fill_arrays(frameRGB->data, frameRGB->linesize,
        frameBuf, AV_PIX_FMT_RGB24, codecCtx->width, codecCtx->height, 1)) < 0) {
        goto end;
    }
    frameRGB->width = codecCtx->width;
    frameRGB->height = codecCtx->height;
    if ((packet = av_packet_alloc()) == NULL) {
        ret = E_OUTOFMEMORY;
        goto end;
    }
    if ((swsCtx = sws_getContext(codecCtx->width, codecCtx->height, codecCtx->pix_fmt,
        codecCtx->width, codecCtx->height, AV_PIX_FMT_RGB24, SWS_FAST_BILINEAR, NULL,
        NULL, NULL)) == NULL) {
        ret = AVERROR_UNKNOWN;
        goto end;
    }
    while (av_read_frame(fmtCtx, packet) >= 0) {
        if (packet->stream_index == streamIdx) {
            // TODO: decode packet.
            if ((ret = decode_packet(codecCtx, packet, frame, frameRGB, swsCtx)) < 0) {
                goto end;
            }
        }
        av_packet_unref(packet);
    }
        
    *hbmp = (HBITMAP)LoadImage(NULL, L"test.bmp",
        IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE);
    ret = S_OK;
end:
    if (swsCtx)
        sws_freeContext(swsCtx);
    if (packet)
        av_packet_free(&packet);
    if (frameBuf)
        av_free(frameBuf);
    if (frameRGB)
        av_frame_free(&frameRGB);
    if (frame)
        av_frame_free(&frame);
    if (codecCtx)
        avcodec_free_context(&codecCtx);
    if (fmtCtx)
        avformat_close_input(&fmtCtx);
    if (avioCtx)
        avio_context_free(&avioCtx);
    return ret;
}

void save_rgb_frame(unsigned char *buf, int wrap, int xsize, int ysize) {
    FILE *f;
    int i;
    f = fopen("test.ppm", "wb");
    fprintf(f, "P6\n%d %d\n255\n", xsize, ysize);

    for (i = 0; i < ysize; i++) {
        unsigned char *ch = (buf + i * wrap);
        //ProcessArray(ch, xsize);
        fwrite(ch, 1, xsize * 3, f);  //Write 3 bytes per pixel.
    }

    fclose(f);
}

static int decode_packet(AVCodecContext *codecCtx, AVPacket *packet, AVFrame *frame,
    AVFrame *frameRGB, struct SwsContext *swsCtx) {
    int ret;
    if ((ret = avcodec_send_packet(codecCtx, packet)) < 0)
        return ret;
    while (ret >= 0) {
        ret = avcodec_receive_frame(codecCtx, frame);
        if (ret == AVERROR(EAGAIN) || ret == AVERROR_EOF)
            return 0;
        if (ret < 0)
            return ret;
        if ((ret = sws_scale(swsCtx, frame->data, frame->linesize,
            0, codecCtx->height, frameRGB->data, frameRGB->linesize)) < 0)
            return ret;
        save_rgb_frame(frameRGB->data[0], frameRGB->linesize[0], frameRGB->width, frameRGB->height);
    }
    return ret;
}

static int get_video_stream_index(AVFormatContext *ic) {
    for (unsigned int i = 0; i < ic->nb_streams; i++) {
        if (ic->streams[i]->codecpar->codec_type == AVMEDIA_TYPE_VIDEO)
            return i;
    }
    return AVERROR_STREAM_NOT_FOUND;
}

static int read_cb(void *opaque, uint8_t *buf, int bufSize) {
    IStream *stream = (IStream*)opaque;
    ULONG numBytes;
    IStream_Read(stream, buf, bufSize, &numBytes);
    if (numBytes == 0)
        return AVERROR_EOF;
    return numBytes;
}

static int64_t seek_cb(void *opaque, int64_t offset, int whence) {
    IStream *stream = (IStream*)opaque;
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