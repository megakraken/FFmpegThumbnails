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

static HRESULT create_format_context(IStream *stream, AVFormatContext **dst);
static int get_video_stream_index(AVFormatContext *ic);
static HRESULT create_codec_context(AVCodecParameters *codecParams, AVCodecContext **dst);
static HRESULT create_rgb_frame(AVCodecContext *codecCtx, int cx, AVFrame **dst);
static int seek_to(int seconds, AVFormatContext *fmtCtx, int streamIdx);
static int decode_packet(AVCodecContext *codecCtx, AVPacket *packet, AVFrame *frame,
    AVFrame *frameRGB, struct SwsContext *swsCtx);
static HRESULT create_bitmap_from_frame(AVFrame *frameRGB, OUT HBITMAP *hbmp);
static void clean_up(struct SwsContext *swsCtx, AVPacket *packet, AVFrame *frameRGB,
    AVFrame *frame, AVCodecContext *codecCtx, AVFormatContext *fmtCtx);

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
    HRESULT ret;
    AVFormatContext *fmtCtx = NULL;
    AVCodecContext *codecCtx = NULL;
    AVFrame *frame = NULL, *frameRGB = NULL;
    AVPacket *packet = NULL;
    int streamIdx;
    struct SwsContext *swsCtx = NULL;
    if ((ret = create_format_context(stream, &fmtCtx)) < 0)
        goto end;
    if ((ret = get_video_stream_index(fmtCtx)) < 0)
        goto end;
    streamIdx = ret;
    if ((ret = create_codec_context(
        fmtCtx->streams[streamIdx]->codecpar, &codecCtx)) < 0) {
        goto end;
    }
    if ((frame = av_frame_alloc()) == NULL) {
        ret = E_OUTOFMEMORY;
        goto end;
    }
    if ((ret = create_rgb_frame(codecCtx, cx, &frameRGB)) < 0)
        goto end;
    if ((packet = av_packet_alloc()) == NULL) {
        ret = E_OUTOFMEMORY;
        goto end;
    }
    if ((swsCtx = sws_getContext(codecCtx->width, codecCtx->height, codecCtx->pix_fmt,
        frameRGB->width, frameRGB->height, AV_PIX_FMT_RGB24, SWS_FAST_BILINEAR, NULL,
        NULL, NULL)) == NULL) {
        ret = AVERROR_UNKNOWN;
        goto end;
    }
    // TODO: Seek to timestamp.
    // Extract frame.
    // Convert to RGB and resize frame.
    // Convert to DIB.
    int seconds = 10;
    seek_to(10, fmtCtx, streamIdx);

    while (av_read_frame(fmtCtx, packet) >= 0) {
        if (packet->stream_index == streamIdx) {
            // TODO: decode packet.
 //           if ((ret = decode_packet(codecCtx, packet, frame, frameRGB, swsCtx)) < 0) {
 //               goto end;
 //           }
            decode_packet(codecCtx, packet, frame, frameRGB, swsCtx);
            create_bitmap_from_frame(frameRGB, hbmp);
            break;
        }
        av_packet_unref(packet);
    }

    ret = S_OK;
end:
    clean_up(swsCtx, packet, frameRGB, frame, codecCtx, fmtCtx);
    return ret;
}

static int read_cb(void *opaque, uint8_t *buf, int bufSize) {
    IStream *stream = (IStream *)opaque;
    ULONG numBytes;
    IStream_Read(stream, buf, bufSize, &numBytes);
    if (numBytes == 0)
        return AVERROR_EOF;
    return numBytes;
}

static int64_t seek_cb(void *opaque, int64_t offset, int whence) {
    IStream *stream = (IStream *)opaque;
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

/**
 * This creates and initializes an AVFormatContext and sets it up to read media data
 * from the given stream.
 * 
 * @param stream  The IStream instance the AVFormatContext will be wired up to.
 * @param dst     A pointer to a pointer that will point to the allocated AVFormatContext
 *                structure upon function return.
 * 
 * @return        If this function succeeds, it returns S_OK. Otherwise, it returns
 *                an HRESULT (or FFMPEG) error code.
 */
static HRESULT create_format_context(IStream *stream, AVFormatContext **dst) {
    HRESULT ret;
    int bufSize = 32768;
    AVIOContext *avioCtx = NULL;
    AVFormatContext *fmtCtx = NULL;
    unsigned char *buf = av_malloc(bufSize + AV_INPUT_BUFFER_PADDING_SIZE);
    if (buf == NULL) {
        ret = E_OUTOFMEMORY;
        goto end;
    }
    if ((avioCtx = avio_alloc_context(buf, bufSize, 0, stream, &read_cb,
        NULL, &seek_cb)) == NULL) {
        ret = E_OUTOFMEMORY;
        goto end;
    }
    if ((fmtCtx = avformat_alloc_context()) == NULL) {
        ret = E_OUTOFMEMORY;
        goto end;
    }
    // This needs to free'd on cleanup.
    fmtCtx->pb = avioCtx;
    if ((ret = avformat_open_input(&fmtCtx, "dummy", NULL, NULL)) != 0)
        goto end;
    if ((ret = avformat_find_stream_info(fmtCtx, NULL)) < 0)
        goto end;
    ret = S_OK;
    *dst = fmtCtx;
end:
    if (ret < 0) {
        if (fmtCtx)
            avformat_close_input(&fmtCtx);
        if (avioCtx) {
            av_free(avioCtx->buffer);
            avio_context_free(&avioCtx);
        }
    }
    return ret;
}

static HRESULT create_codec_context(AVCodecParameters *codecParams, AVCodecContext **dst) {
    HRESULT ret;
    const AVCodec *codec;
    AVCodecContext *codecCtx = NULL;
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
    ret = S_OK;
    *dst = codecCtx;
end:
    if (ret < 0) {
        if (codecCtx)
            avcodec_free_context(&codecCtx);
    }
    return ret;
}

static HRESULT create_rgb_frame(AVCodecContext *codecCtx, int cx, AVFrame **dst) {
    HRESULT ret;
    AVFrame *frameRGB = NULL;
    uint8_t *frameBuf = NULL;
    float aspect = codecCtx->width / (float)codecCtx->height;
    int frameWidth, frameHeight;
    if (codecCtx->width > codecCtx->height) {
        // landscape
        frameWidth = cx;
        frameHeight = cx / aspect;
    } else {
        // portrait
        frameHeight = cx;
        frameWidth = cx * aspect;
    }
    if ((frameRGB = av_frame_alloc()) == NULL) {
        ret = E_OUTOFMEMORY;
        goto end;
    }
    if ((ret = av_image_get_buffer_size(AV_PIX_FMT_RGB24, frameWidth, frameHeight, 1)) < 0)
        goto end;
    frameBuf = av_malloc(ret);
    // This will set up frameRGB->data[0] to point at the same address as frameBuf
    // is pointing to, so when cleaning up we can just free frameRGB->data[0] and
    // don't need to pass frameBuf around.
    if ((ret = av_image_fill_arrays(frameRGB->data, frameRGB->linesize,
        frameBuf, AV_PIX_FMT_RGB24, frameWidth, frameHeight, 1)) < 0) {
        goto end;
    }
    frameRGB->width = frameWidth;
    frameRGB->height = frameHeight;
    ret = S_OK;
    *dst = frameRGB;
end:
    if (ret < 0) {
        if (frameBuf)
            av_free(frameBuf);
        if (frameRGB)
            av_frame_free(&frameRGB);
    }
    return ret;
}

static int seek_to(int seconds, AVFormatContext *fmtCtx, int streamIdx) {
    int64_t ts = av_rescale(
        seconds,
        fmtCtx->streams[streamIdx]->time_base.den,
        fmtCtx->streams[streamIdx]->time_base.num
    );
    return avformat_seek_file(fmtCtx, streamIdx, 0, ts, ts, 0);
}

static HRESULT create_bitmap_from_frame(AVFrame *frameRGB, OUT HBITMAP *hbmp) {
    HRESULT ret;
    unsigned char *bits;
    int i, c, y;
    BITMAPINFO bmi = { 0 };
    bmi.bmiHeader.biSize = sizeof(bmi.bmiHeader);
    bmi.bmiHeader.biWidth = frameRGB->width;
    // MSDN: If biHeight is negative, the bitmap is a top-down DIB with the origin at the
    //       upper left corner.
    bmi.bmiHeader.biHeight = -frameRGB->height;
    bmi.bmiHeader.biPlanes = 1;
    bmi.bmiHeader.biBitCount = 32;
    bmi.bmiHeader.biCompression = BI_RGB;
    *hbmp = NULL;
    if ((*hbmp = CreateDIBSection(NULL, &bmi, DIB_RGB_COLORS, &bits, NULL, 0)) == NULL) {
        ret = GetLastError();
        goto end;
    }
    unsigned int *bytes = bits;
    for (i = 0; i < frameRGB->height; i++) {
        // linesize is just the size of a single line in bytes, i.e. for a 24-bit image
        // linesize = width * 3.
        unsigned char *ch = (frameRGB->data[0] + i * frameRGB->linesize[0]);
        for (c = 0; c < frameRGB->width; c++) {
            bytes[i * frameRGB->width + c] = (ch[c * 3 + 0] << 16) | (ch[c * 3 + 1] << 8)
                | (ch[c * 3 + 2]);
        }
    }
    ret = S_OK;
end:
    if (ret < 0) {
        if (*hbmp)
            DeleteObject(*hbmp);
    }
    return ret;
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

        return 0;
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

static void clean_up(struct SwsContext *swsCtx, AVPacket *packet, AVFrame *frameRGB,
    AVFrame *frame, AVCodecContext *codecCtx, AVFormatContext *fmtCtx) {
    if (swsCtx)
        sws_freeContext(swsCtx);
    if (packet)
        av_packet_free(&packet);
    if (frameRGB) {
        av_free(frameRGB->data[0]);
        av_frame_free(&frameRGB);
    }
    if (frame)
        av_frame_free(&frame);
    if (codecCtx)
        avcodec_free_context(&codecCtx);
    if (fmtCtx) {
        // We must also clean up the associated AVIOContext.
        if (fmtCtx->pb) {
            av_free(fmtCtx->pb->buffer);
            avio_context_free(&fmtCtx->pb);
        }
        avformat_close_input(&fmtCtx);
    }
}