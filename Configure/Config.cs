using System.Collections.Generic;

namespace Configure {
    public static class Config {
        const string Clsid = "{8D60D8ED-AC78-444B-9FC8-DDE8240A2A9B}";

        public static void Install() {
            // 1. Invoke  regsvr32.exe
            // 2. Restart explorer.exe
        }

        public static void Uninstall() {
            // 1. Invoke  regsvr32.exe
            // 2. Restart explorer.exe
        }

        public static void ClearThumbnailCache() {
            // 1. Invalidate Windows Thumbnail Cache
            // 2. Restart explorer.exe
        }

        public static string GetInstallationPath() {
            // Read stuff from registry.
            return string.Empty;
        }

        public static int GetThumbnailTimestamp() {
            // Read stuff from registry.
            return -1;
        }

        public static void SetThumbnailTimestamp() {
            // Set stuff in registry.
        }

        public static IDictionary<string, bool> GetFileAssociations() {
            // Read stuff from registry.
            return null;
        }

        public static void SetFileAssociations(IDictionary<string, bool> dict) {
            // Set stuff in registry.
        }
    }
}
