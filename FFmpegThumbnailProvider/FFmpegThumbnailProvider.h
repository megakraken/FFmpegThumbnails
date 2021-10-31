#ifndef _FFMPEGTHUMBNAILPROVIDER_H_
#define _FFMPEGTHUMBNAILPROVIDER_H_

#include <objbase.h>
#include <shlwapi.h>
#include <thumbcache.h>
#include <shlobj.h>
#include <new>
#include "../FFmpegThumbnail/FFmpegThumbnail.h"

#pragma comment(lib, "shlwapi.lib")
// I have no idea what this warning is supposed to tell me and StackOverflow doesn't
// know either.
#pragma warning(disable : 28251)

#define SZ_CLSID_FFMPEGTHUMBHANDLER     L"{8D60D8ED-AC78-444B-9FC8-DDE8240A2A9B}"
#define SZ_FFMPEGTHUMBHANDLER           L"FFmpeg Thumbnail Handler"

const CLSID CLSID_FFmpegThumbHandler = { 0x8d60d8ed, 0xac78, 0x444b,
    { 0x9f, 0xc8, 0xdd, 0xe8, 0x24, 0xa, 0x2a, 0x9b } };

HRESULT FFmpegThumbProvider_CreateInstance(REFIID riid, void **ppv);

#endif /* _FFMPEGTHUMBNAILPROVIDER_H_ */