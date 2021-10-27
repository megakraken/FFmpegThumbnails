#ifndef _FFMPEGTHUMBNAIL_H_
#define _FFMPEGTHUMBNAIL_H_

#include <Windows.h>

HRESULT GetThumbnail(IN IStream* stream, int cx, OUT HBITMAP* thumbnail);

#endif /* _FFMPEGTHUMBNAIL_H_ */