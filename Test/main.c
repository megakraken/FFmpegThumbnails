#include <stdio.h>
#include "../FFmpegThumbnail/FFmpegThumbnail.h"
#include <Shlwapi.h>

#pragma comment(lib, "shlwapi.lib")

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
    // TODO: Save HBitmap as .bmp file...
    printf("hello\n");
}