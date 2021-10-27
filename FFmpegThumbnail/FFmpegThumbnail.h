#ifndef _FFMPEGTHUMBNAIL_H_
#define _FFMPEGTHUMBNAIL_H_

#include <Windows.h>

#ifdef __cplusplus
extern "C" {
#endif
    HRESULT GetThumbnail(IN IStream* stream, int cx, OUT HBITMAP* thumbnail);

#ifdef __cplusplus
}
#endif  /* __cplusplus */

#endif /* _FFMPEGTHUMBNAIL_H_ */