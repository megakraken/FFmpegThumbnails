/**
 * Copyright (C) 2021 megakraken
 *
 * FFmpegThumbnails is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * FFmpegThumbnails is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with FFmpeg; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA
 */

#ifndef _FFMPEGTHUMBNAIL_H_
#define _FFMPEGTHUMBNAIL_H_

#include <Windows.h>

/**
 * Generates thumbnail from first frame of the video.
 */
#define TS_FIRSTFRAME   0x80000001
/**
 * Generates thumbnail after first few seconds of video.
 */
#define TS_BEGINNING    0x80000002
/**
 * Generates thumbnail from the middle of the video.
 */
#define TS_MIDDLE       0x80000003

#ifdef __cplusplus
extern "C" {
#endif
    /**
     * Gets a thumbnail for the video contained in the given stream.
     *
     * @param stream  A pointer to an IStream interface that represents the stream
     *                source that contains the video.
     * @param cx      The maximum thumbnail size, in pixels. The returned bitmap should
     *                fit into a square of width and height cx, though it does not need
     *                to be a square image.
     * @param ts      The timestamp at which to generate the thumbnail within the video
     *                file. Can be either a positive value to denote an offset in seconds,
     *                or one of the following values:
     *                TS_FIRSTFRAME - Generate thumbnail from the very first video frame.
     *                TS_BEGINNING  - Generate thumbnail from the beginning of the video.
     *                TS_MIDDLE     - Generate thumbnail from the middle of the video.
     * @param hbmp    When this function returns, contains a pointer to the thumbnail
     *                image handle. The image is a DIB section and 32 bits per pixel.
     *
     * @return        If this function succeeds, it returns S_OK. Otherwise, it returns
     *                an HRESULT (or FFMPEG) error code.
     */
    HRESULT GetVideoThumbnail(IN IStream *stream, int cx, int ts, OUT HBITMAP *thumbnail);
#ifdef __cplusplus
}
#endif  /* __cplusplus */

#endif /* _FFMPEGTHUMBNAIL_H_ */