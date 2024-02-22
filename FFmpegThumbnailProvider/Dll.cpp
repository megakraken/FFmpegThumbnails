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
  * Implements the COM plumbing needed for implementing the thumbnail provider.
  *
  * Based on the RecipeThumbnailProvider sample from MSDN.
  *
  */

#include "FFmpegThumbnailProvider.h"

typedef HRESULT(*PFNCREATEINSTANCE)(REFIID riid, void **ppvObject);
struct CLASS_OBJECT_INIT {
    const CLSID *pClsid;
    PFNCREATEINSTANCE pfnCreate;
};

// add classes supported by this module here
const CLASS_OBJECT_INIT c_rgClassObjectInit[] =
{
    { &CLSID_FFmpegThumbHandler, FFmpegThumbProvider_CreateInstance }
};


long g_cRefModule = 0;

// Handle the the DLL's module
HINSTANCE g_hInst = NULL;

// Standard DLL functions
STDAPI_(BOOL) DllMain(HINSTANCE hInstance, DWORD dwReason, void *) {
    if (dwReason == DLL_PROCESS_ATTACH) {
        g_hInst = hInstance;
        DisableThreadLibraryCalls(hInstance);
    }
    return TRUE;
}

STDAPI DllCanUnloadNow() {
    // Only allow the DLL to be unloaded after all outstanding references have been released
    return (g_cRefModule == 0) ? S_OK : S_FALSE;
}

void DllAddRef() {
    InterlockedIncrement(&g_cRefModule);
}

void DllRelease() {
    InterlockedDecrement(&g_cRefModule);
}

class CClassFactory : public IClassFactory {
public:
    static HRESULT CreateInstance(REFCLSID clsid, const CLASS_OBJECT_INIT *pClassObjectInits,
        size_t cClassObjectInits, REFIID riid, void **ppv) {
        *ppv = NULL;
        HRESULT hr = CLASS_E_CLASSNOTAVAILABLE;
        for (size_t i = 0; i < cClassObjectInits; i++) {
            if (clsid == *pClassObjectInits[i].pClsid) {
                IClassFactory *pClassFactory =
                    new (std::nothrow) CClassFactory(pClassObjectInits[i].pfnCreate);
                hr = pClassFactory ? S_OK : E_OUTOFMEMORY;
                if (SUCCEEDED(hr)) {
                    hr = pClassFactory->QueryInterface(riid, ppv);
                    pClassFactory->Release();
                }
                break; // match found
            }
        }
        return hr;
    }

    CClassFactory(PFNCREATEINSTANCE pfnCreate) : _cRef(1), _pfnCreate(pfnCreate) {
        DllAddRef();
    }

    // IUnknown
    IFACEMETHODIMP QueryInterface(REFIID riid, void **ppv) {
        static const QITAB qit[] = {
            QITABENT(CClassFactory, IClassFactory),
            { 0 }
        };
        return QISearch(this, qit, riid, ppv);
    }

    IFACEMETHODIMP_(ULONG) AddRef() {
        return InterlockedIncrement(&_cRef);
    }

    IFACEMETHODIMP_(ULONG) Release() {
        long cRef = InterlockedDecrement(&_cRef);
        if (cRef == 0) {
            delete this;
        }
        return cRef;
    }

    // IClassFactory
    IFACEMETHODIMP CreateInstance(IUnknown *punkOuter, REFIID riid, void **ppv) {
        return punkOuter ? CLASS_E_NOAGGREGATION : _pfnCreate(riid, ppv);
    }

    IFACEMETHODIMP LockServer(BOOL fLock) {
        if (fLock) {
            DllAddRef();
        } else {
            DllRelease();
        }
        return S_OK;
    }

private:
    ~CClassFactory() {
        DllRelease();
    }

    long _cRef;
    PFNCREATEINSTANCE _pfnCreate;
};

STDAPI DllGetClassObject(REFCLSID clsid, REFIID riid, void **ppv) {
    return CClassFactory::CreateInstance(clsid, c_rgClassObjectInit,
        ARRAYSIZE(c_rgClassObjectInit), riid, ppv);
}

// A struct to hold the information required for a registry entry
struct REGISTRY_ENTRY {
    HKEY   hkeyRoot;
    PCWSTR pszKeyName;
    PCWSTR pszValueName;
    DWORD  dwType;
    LPVOID lpData;
//    PCWSTR pszData;
};

// Creates a registry key (if needed) and sets the default value of the key
HRESULT CreateRegKeyAndSetValue(const REGISTRY_ENTRY *pRegistryEntry) {
    HKEY hKey;
    HRESULT hr = HRESULT_FROM_WIN32(RegCreateKeyExW(pRegistryEntry->hkeyRoot,
        pRegistryEntry->pszKeyName, 0, NULL, REG_OPTION_NON_VOLATILE, KEY_SET_VALUE, NULL,
        &hKey, NULL));
    if (SUCCEEDED(hr)) {
        DWORD cbData = pRegistryEntry->dwType == REG_DWORD ? sizeof(DWORD) :
            ((DWORD)wcslen((PCWSTR)pRegistryEntry->lpData) + 1) * sizeof(WCHAR);
        DWORD dwValue = (DWORD)pRegistryEntry->lpData;
        LPBYTE lpData =  pRegistryEntry->dwType == REG_DWORD ? (LPBYTE)&dwValue :
            (LPBYTE)pRegistryEntry->lpData;
        hr = HRESULT_FROM_WIN32(RegSetValueExW(hKey, pRegistryEntry->pszValueName, 0,
            pRegistryEntry->dwType, lpData, cbData));
        RegCloseKey(hKey);
    }
    return hr;
}

//
// Registers this COM server
//
STDAPI DllRegisterServer() {
    HRESULT hr;
    WCHAR szModuleName[MAX_PATH];
    if (!GetModuleFileNameW(g_hInst, szModuleName, ARRAYSIZE(szModuleName))) {
        hr = HRESULT_FROM_WIN32(GetLastError());
    } else {
        // List of registry entries we want to create
        const REGISTRY_ENTRY rgRegistryEntries[] = {
            {
                HKEY_CURRENT_USER,
                L"Software\\Classes\\CLSID\\" SZ_CLSID_FFMPEGTHUMBHANDLER,
                NULL,
                REG_SZ,
                (LPVOID)SZ_FFMPEGTHUMBHANDLER
            },
            {
                HKEY_CURRENT_USER,
                L"Software\\Classes\\CLSID\\" SZ_CLSID_FFMPEGTHUMBHANDLER,
                L"ThumbnailTimestamp",
                REG_DWORD,
                (LPVOID)TS_BEGINNING
            },
            {
                HKEY_CURRENT_USER,
                L"Software\\Classes\\CLSID\\" SZ_CLSID_FFMPEGTHUMBHANDLER L"\\InProcServer32",
                NULL,
                REG_SZ,
                szModuleName
            },
            {
                HKEY_CURRENT_USER,
                L"Software\\Classes\\CLSID\\" SZ_CLSID_FFMPEGTHUMBHANDLER L"\\InProcServer32",
                L"ThreadingModel",
                REG_SZ,
                (LPVOID)L"Apartment"
            },
            //{
            //    HKEY_CURRENT_USER,
            //    L"Software\\Classes\\.recipe\\ShellEx\\{e357fccd-a995-4576-b01f-234630154e96}",
            //    NULL,
            //    SZ_CLSID_FFMPEGTHUMBHANDLER
            //},
        };
        hr = S_OK;
        for (int i = 0; i < ARRAYSIZE(rgRegistryEntries) && SUCCEEDED(hr); i++) {
            hr = CreateRegKeyAndSetValue(&rgRegistryEntries[i]);
        }
    }
    if (SUCCEEDED(hr)) {
        // This tells the shell to invalidate the thumbnail cache.  This is important because
        // any .recipe files viewed before registering this handler would otherwise show
        // cached blank thumbnails.
        SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, NULL, NULL);
    }
    return hr;
}

//
// Unregisters this COM server
//
STDAPI DllUnregisterServer() {
    HRESULT hr = S_OK;
    const PCWSTR rgpszKeys[] = {
        L"Software\\Classes\\CLSID\\" SZ_CLSID_FFMPEGTHUMBHANDLER,
        //L"Software\\Classes\\.recipe\\ShellEx\\{e357fccd-a995-4576-b01f-234630154e96}"
    };
    // Delete the registry entries
    for (int i = 0; i < ARRAYSIZE(rgpszKeys) && SUCCEEDED(hr); i++) {
        hr = HRESULT_FROM_WIN32(RegDeleteTreeW(HKEY_CURRENT_USER, rgpszKeys[i]));
        if (hr == HRESULT_FROM_WIN32(ERROR_FILE_NOT_FOUND)) {
            // If the registry entry has already been deleted, say S_OK.
            hr = S_OK;
        }
    }
    return hr;
}
