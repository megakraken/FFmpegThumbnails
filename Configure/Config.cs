﻿/**
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

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace Configure {
    /// <summary>
    /// Implements installation and uninstallation of the FFmpegThumbnailProvider component
    /// as well as some other configuration options.
    /// </summary>
    public static class Config {
        static readonly string[] Extensions = {
            "3g2", "3gp", "asf", "avi", "flv", "m4v", "mkv", "mov", "mp4", "m4p", "mpg",
            "mp2", "mpe", "mpeg", "mpv", "m2v", "ts", "ogv", "vob", "webm", "wmv", "qt"
        };

        [DllImport("Shell32.dll")]
        static extern int SHChangeNotify(int eventId, uint flags, IntPtr dwItem1, IntPtr dwItem2);

        /// <summary>
        /// The CLSID of our FFmpegThumbnailProvider.
        /// </summary>
        const string Clsid = "{8D60D8ED-AC78-444B-9FC8-DDE8240A2A9B}";

        /// <summary>
        /// The registration path of our IThumbnailProvider COM component.
        /// </summary>
        const string RegPath = @"HKEY_CURRENT_USER\Software\Classes\CLSID\" + Clsid;

        /// <summary>
        /// The name of the dll that implements the COM class.
        /// </summary>
        const string Dll = "FFmpegThumbnailProvider.dll";

        /// <summary>
        /// Installs the FFmpegThumbnailProvider with Windows Explorer.
        /// </summary>
        public static void Install() {
            // 1. Extract the embedded files into our application's AppData directory.
            ExtractResources();
            // 2. Invoke regsvr32.exe
            var ret = InvokeRegSvr32(true);
            if (ret != 0)
                throw new Exception($"Registration of {Dll} failed with error-code {ret}.");
            // 3. Restart explorer.exe
            RestartExplorer();
        }

        /// <summary>
        /// Uninstalls the FFmpegThumbnailProvider.
        /// </summary>
        public static void Uninstall() {
            // 1. Invoke regsvr32.exe
            var ret = InvokeRegSvr32(false);
            if (ret != 0)
                throw new Exception($"Unregistration of {Dll} failed with error-code {ret}.");
            // 2. Clear/Restore any file-type associations.
            foreach (var ext in Extensions)
                SetFileAssociation(ext, false);
            // 3. Restart explorer.exe
            RestartExplorer();
            // 4. Remove Application Directory.
            Directory.Delete(GetAppDirectory(), true);
        }

        /// <summary>
        /// Clears the Windows Thumbnail Cache so that thumbnails for file-types will be
        /// regenerated by their respective IThumbnailProvider.
        /// </summary>
        public static void ClearThumbnailCache() {
            const int SHCNE_ASSOCCHANGED = 0x08000000;
            const int SHCNF_FLUSH = 0x1000;
            // I thought this was enough to invalidate the Thumbnail Cache but it doesn't
            // seem to be doing anything.
            SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_FLUSH, IntPtr.Zero, IntPtr.Zero);
            // Windows caches generated thumbnails in .db files saved under
            // AppData\Local\Microsoft\Windows\Explorer.
            // Sadly it's a PITA to try to delete them because Explorer keeps them locked and
            // you can't just kill Explorer because Windows will automatically restart it so
            // we have to use the Restart Manager API to do it.
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Microsoft", "Windows", "Explorer"
            );
            using (var rm = new RestartManagerSession()) {
                rm.RegisterProcess(Process.GetProcessesByName("explorer"));
                try {
                    rm.Shutdown(RestartManagerSession.ShutdownType.Normal);
                    Thread.Sleep(100);
                    foreach (var f in Directory.GetFiles(path, "*.db"))
                        File.Delete(f);
                    Thread.Sleep(100);
                } finally {
                    rm.Restart();
                }
            }
        }

        /// <summary>
        /// Returns the current installation path of our IThumbnailProvider component.
        /// </summary>
        /// <returns>
        /// The installation path of the FFmpegThumbnailProvider or null if it has not been
        /// installed/registered.
        /// </returns>
        public static string GetInstallationPath() {
            var value = Registry.GetValue(RegPath + @"\InProcServer32", null, null);
            if (value == null)
                return null;
            return Path.GetDirectoryName(value.ToString());
        }

        /// <summary>
        /// Gets the value of the ThumbnailTimestamp Registry setting that determines how
        /// thumbnails are generated for video files.
        /// </summary>
        /// <returns>
        /// The ThumbnailTimestamp value as stored in the Registry.
        /// </returns>
        public static int GetThumbnailTimestamp() {
            var val = Registry.GetValue(RegPath, "ThumbnailTimestamp", -1);
            if (val == null)
                return -1;
            return (int)val;
        }

        /// <summary>
        /// Sets the value of the ThumbnailTimestamp Registry setting to the specified
        /// value.
        /// </summary>
        /// <param name="value">
        /// The value to set the ThumbnailTimestamp Registry setting to.
        /// </param>
        public static void SetThumbnailTimestamp(int value) {
            Registry.SetValue(RegPath, "ThumbnailTimestamp", value);
        }

        /// <summary>
        /// Gets a map of file-type associations for the thumbnail provider.
        /// </summary>
        /// <returns>
        /// A map of file-type associations with the file extension as key and a bool as
        /// value that is true if the respective file-type is associated with our
        /// FFmpegThumbnailProvider, or false otherwise.
        /// </returns>
        public static IDictionary<string, bool> GetFileAssociations() {
            var ret = new Dictionary<string, bool>();
            foreach (var ext in Extensions)
                ret[ext] = IsAssociated(ext);
            return ret;
        }

        /// <summary>
        /// Sets the specified file-type to be associated or dissociated with our
        /// FFmpegThumbnailProvder.
        /// </summary>
        /// <param name="ext">
        /// The extension of the file-type to configure.
        /// </param>
        /// <param name="associate">
        /// true to associate the file-type to our IThumbnailProvider or false to
        /// dissociate it.
        /// </param>
        public static void SetFileAssociation(string ext, bool associate) {
            var path = Path.Combine(GetAppDirectory(), "Registry");
            Directory.CreateDirectory(path);
            var backup = Path.Combine(path, $"{ext}.old");
            var current = IsAssociated(ext);
            // Set association and save old value.
            if (!current && associate) {
                var old = SetAssociated(ext, Clsid);
                if (old != null)
                    File.WriteAllText(backup, old);
            } else if (current && !associate) {
                // Restore old value.
                if (File.Exists(backup)) {
                    var old = File.ReadAllText(backup);
                    SetAssociated(ext, old);
                    File.Delete(backup);
                } else {
                    SetAssociated(ext, null);
                }
            }
        }

        /// <summary>
        /// Invokes the 'regsvr32' utility and returns it's exit-code.
        /// </summary>
        /// <param name="install">
        /// true to install the DLL, or false to uninstall it.
        /// </param>
        /// <returns>
        /// The exit-code returned by the 'regsvr32' process.
        /// </returns>
        static int InvokeRegSvr32(bool install) {
            using (var p = new Process()) {
                var path = Path.Combine(
                    GetAppDirectory(), Dll
                );
                p.StartInfo.FileName = "regsvr32.exe";
                p.StartInfo.Arguments = install ? $"/s {path}" : $"/s /u {path}";
                p.StartInfo.UseShellExecute = false;
                p.Start();
                p.WaitForExit();
                return p.ExitCode;
            }
        }

        /// <summary>
        /// Restarts the Windows explorer process.
        /// </summary>
        public static void RestartExplorer() {
            Process.GetProcessesByName("explorer");
            foreach (var p in Process.GetProcessesByName("explorer"))
                p.Kill();
            // Windows automatically restarts explorer when you kill it...
//            Process.Start("explorer.exe");
        }

        /// <summary>
        /// Gets whether the specified file extension is associated to our
        /// IThumbnailProvider.
        /// </summary>
        /// <param name="extension">
        /// The file extension.
        /// </param>
        /// <returns>
        /// true if the file extension is associated to the FFmpegThumbnailProvider; otherwise
        /// false.
        /// </returns>
        static bool IsAssociated(string extension) {
            return Clsid.Equals(GetAssociated(extension),
                StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Gets the CLSID of the IThumbnailProvider associated with the specified extension.
        /// </summary>
        /// <param name="extension">
        /// The extension to get the CLSID of the associated IThumbnailProvider for.
        /// </param>
        /// <returns>
        /// The CLSID of the associated IThumbnailProvider or null if none is configured.
        /// </returns>
        static string GetAssociated(string extension) {
            // {E357FCCD-A995-4576-B01F-234630154E96} is the CLSID for
            // IThumbnailProvider implementations.
            var path = $@"HKEY_CURRENT_USER\Software\Classes\.{extension}\ShellEx\" +
                "{e357fccd-a995-4576-b01f-234630154e96}";
            return Registry.GetValue(path, null, null)?.ToString();
        }

        /// <summary>
        /// Sets the IThumbnailProvider that is used by Explorer for the specified extension
        /// to the specified value.
        /// </summary>
        /// <param name="extension">
        /// The extension to set the IThumbnailProvider CLSID for.
        /// </param>
        /// <param name="value">
        /// The CLSID of the IThumbnailProvider implementation to use.
        /// </param>
        /// <returns>
        /// The CLSID of the old IThumbnailProvider if any was configured, or null if none
        /// was configured.
        /// </returns>
        static string SetAssociated(string extension, string value) {
            // {E357FCCD-A995-4576-B01F-234630154E96} is the CLSID for
            // IThumbnailProvider implementations.
            var path = $@"Software\Classes\.{extension}\ShellEx\" +
                "{e357fccd-a995-4576-b01f-234630154e96}";
            var key = $@"HKEY_CURRENT_USER\{path}";
            var old = Registry.GetValue(key, null, null)?.ToString();
            if (value != null) {
                Registry.SetValue(key, null, value);
            } else {
                // The key must be deleted for Windows to fall back to the
                // default provider.
                Registry.CurrentUser.DeleteSubKey(path);
            }
            return old;
        }

        /// <summary>
        /// Returns the application directory where we keep our files.
        /// </summary>
        /// <returns>
        /// The application directory under appData.
        /// </returns>
        static string GetAppDirectory() {
            return Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData
                ),
                "FFmpegThumbnailProvider"
            );
        }

        /// <summary>
        /// Extracts the embedded FFmpeg binaries and our FFmpegThumbnailHandler DLL into
        /// our application directory.
        /// </summary>
        static void ExtractResources() {
            var path = GetAppDirectory();
            Directory.CreateDirectory(path);
            var asm = Assembly.GetExecutingAssembly();
            foreach (var n in asm.GetManifestResourceNames()) {
                var m = Regex.Match(n, $@"^{nameof(Configure)}\.Resources\.(.+)");
                if (!m.Success)
                    continue;
                var filename = m.Groups[1].Value;
                var dstPath = Path.Combine(path, filename);
                using (var fs = File.Create(dstPath)) {
                    using (var s = asm.GetManifestResourceStream(n))
                        s.CopyTo(fs);
                }
            }
        }
    }
}
