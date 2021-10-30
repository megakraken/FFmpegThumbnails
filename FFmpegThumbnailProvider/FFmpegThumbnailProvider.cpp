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

/**
  * Implements the actual IThumbnailProvider.
  *
  * Based on the RecipeThumbnailProvider sample from MSDN.
  *
  */

#include <shlwapi.h>
#include <thumbcache.h>
#include <new>
#include "../FFmpegThumbnail/FFmpegThumbnail.h"


// this thumbnail provider implements IInitializeWithStream to enable being hosted
// in an isolated process for robustness
class FFmpegThumbnailProvider : public IInitializeWithStream,
    public IThumbnailProvider {
public:
    FFmpegThumbnailProvider() : _cRef(1), _pStream(NULL) {
    }

    virtual ~FFmpegThumbnailProvider() {
        if (_pStream) {
            _pStream->Release();
        }
    }

    // IUnknown
    IFACEMETHODIMP QueryInterface(REFIID riid, void **ppv) {
        static const QITAB qit[] =
        {
            QITABENT(FFmpegThumbnailProvider, IInitializeWithStream),
            QITABENT(FFmpegThumbnailProvider, IThumbnailProvider),
            { 0 },
        };
        return QISearch(this, qit, riid, ppv);
    }

    IFACEMETHODIMP_(ULONG) AddRef() {
        return InterlockedIncrement(&_cRef);
    }

    IFACEMETHODIMP_(ULONG) Release() {
        ULONG cRef = InterlockedDecrement(&_cRef);
        if (!cRef) {
            delete this;
        }
        return cRef;
    }

    // IInitializeWithStream
    IFACEMETHODIMP Initialize(IStream *pStream, DWORD grfMode);

    // IThumbnailProvider
    IFACEMETHODIMP GetThumbnail(UINT cx, HBITMAP *phbmp, WTS_ALPHATYPE *pdwAlpha);

private:
    long _cRef;
    IStream *_pStream;     // provided during initialization.
};

HRESULT FFmpegThumbProvider_CreateInstance(REFIID riid, void **ppv) {
    FFmpegThumbnailProvider *pNew = new (std::nothrow) FFmpegThumbnailProvider();
    HRESULT hr = pNew ? S_OK : E_OUTOFMEMORY;
    if (SUCCEEDED(hr)) {
        hr = pNew->QueryInterface(riid, ppv);
        pNew->Release();
    }
    return hr;
}

// IInitializeWithStream
IFACEMETHODIMP FFmpegThumbnailProvider::Initialize(IStream *pStream, DWORD) {
    HRESULT hr = E_UNEXPECTED;  // can only be inited once
    if (_pStream == NULL) {
        // take a reference to the stream if we have not been inited yet
        hr = pStream->QueryInterface(&_pStream);
    }
    return hr;
}

// IThumbnailProvider
IFACEMETHODIMP FFmpegThumbnailProvider::GetThumbnail(UINT cx, HBITMAP *phbmp,
    WTS_ALPHATYPE *pdwAlpha) {
    return GetVideoThumbnail(_pStream, cx, TS_BEGINNING, phbmp);
}
