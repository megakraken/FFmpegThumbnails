#include <stdio.h>
#include "../FFmpegThumbnail/FFmpegThumbnail.h"
#include <Shlwapi.h>
#include <windows.h>
#include <gdiplus.h>
using namespace Gdiplus;
#pragma comment(lib, "shlwapi.lib")
#pragma comment(lib, "gdiplus.lib")

int SaveThumbnail(HBITMAP hThumbnail, const WCHAR* filename);

int main(int argc, char* argv[]) {
    int ret, size;
    IStream* stream;
    HBITMAP bmp;
    if (argc < 3) {
        printf("You need to specify a size and a media file.\n");
        printf("test.exe <size> <path-to-media-file>\n");
        return -1;
    }
    size = atoi(argv[1]);
    if ((ret = SHCreateStreamOnFileA(argv[2], STGM_READ, &stream)) != S_OK) {
        printf("Couldn't open file %s\n", argv[2]);
        return -1;
    }
    if ((ret = GetThumbnail(stream, size, &bmp)) != S_OK) {
        printf("Error creating thumbnail: %i\n", ret);
        return -1;
    }
    const WCHAR* name = L"thumbnail.png";
    if (SaveThumbnail(bmp, name) != S_OK)
        printf("Error writing thumbnail: %i\n", ret);
    else
        wprintf(L"Thumbnail written as %ws\n", name);
}

// See here: https://stackoverflow.com/a/24645420
int GetEncoderClsid(const WCHAR* format, CLSID* pClsid) {
    UINT num = 0, size = 0;
    ImageCodecInfo* pImageCodecInfo = NULL;
    GetImageEncodersSize(&num, &size);
    if (size == 0)
        return -1; 
    pImageCodecInfo = (ImageCodecInfo*)(malloc(size));
    if (pImageCodecInfo == NULL)
        return -1;
    GetImageEncoders(num, size, pImageCodecInfo);
    for (UINT j = 0; j < num; ++j) {
        if (wcscmp(pImageCodecInfo[j].MimeType, format) == 0) {
            *pClsid = pImageCodecInfo[j].Clsid;
            free(pImageCodecInfo);
            return j;
        }
    }
    free(pImageCodecInfo);
    return -1;
}

int SaveThumbnail(HBITMAP hThumbnail, const WCHAR* filename) {
    GdiplusStartupInput gdiplusStartupInput;
    ULONG_PTR gdiplusToken;
    GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, NULL);
    int ret = 0;
    Bitmap* image = new Bitmap(hThumbnail, NULL);
    CLSID myClsId;
    if((ret = GetEncoderClsid(L"image/png", &myClsId)) < 0)
        goto end;
    if (image->Save(filename, &myClsId, NULL) != Status::Ok)
        ret = -1;
    else
        ret = S_OK;
end:
    delete image;
    GdiplusShutdown(gdiplusToken);
    return ret;
}
