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

#define TS_FIRSTFRAME   0x80000001
#define TS_BEGINNING    0x80000002
#define TS_MIDDLE       0x80000003

#ifdef __cplusplus
extern "C" {
#endif
    HRESULT GetThumbnail(IN IStream *stream, int cx, int ts, OUT HBITMAP *thumbnail);
#ifdef __cplusplus
}
#endif  /* __cplusplus */

#endif /* _FFMPEGTHUMBNAIL_H_ */