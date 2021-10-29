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