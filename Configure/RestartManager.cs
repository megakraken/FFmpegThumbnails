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

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;

// See here: https://stackoverflow.com/a/53231786
namespace Configure {
    /// <summary>
    /// Represents a Restart Manager session. The Restart Manager enables all but the critical
    /// system services to be shut down and restarted with aim of eliminate or reduce the number
    /// of system restarts that are required to complete an installation or update.
    /// </summary>
    /// <remarks>
    /// We need this to shutdown Explorer without it automatically immediately restarting,
    /// so that we can delete the Thumbnail cache files that Explorer keeps a lock on.
    /// </remarks>
    public class RestartManagerSession : IDisposable {
        public delegate void WriteStatusCallback(uint percentageCompleted);

        /// <summary>
        /// Specifies the type of modification that is applied to restart or shutdown actions.
        /// </summary>
        public enum FilterAction : uint {
            /// <summary>
            /// Prevents the restart of the specified application or service.
            /// </summary>
            RmNoRestart = 1,

            /// <summary>
            /// Prevents the shut down and restart of the specified application or service.
            /// </summary>
            RmNoShutdown = 2
        }

        [Flags]
        public enum ShutdownType : uint {
            /// <summary>
            /// Default behavior
            /// </summary>
            Normal = 0,

            /// <summary>
            /// Force unresponsive applications and services to shut down after the timeout
            /// period. An application that does not respond to a shutdown request is forced to
            /// shut down within 30 seconds. A service that does not respond to a shutdown
            /// request is forced to shut down after 20 seconds.
            /// </summary>
            ForceShutdown = 0x1,

            /// <summary>
            /// Shut down applications if and only if all the applications have been registered
            /// for restart using the RegisterApplicationRestart function. If any processes or
            /// services cannot be restarted, then no processes or services are shut down.
            /// </summary>
            ShutdownOnlyRegistered = 0x10
        }

        public RestartManagerSession() {
            SessionKey = Guid.NewGuid().ToString();
            var ret = StartSession(out var sessionHandle, 0, SessionKey);
            if (ret != 0)
                throw new Win32Exception(ret);
            Handle = sessionHandle;
        }

        public RestartManagerSession(string sessionKey) {
            SessionKey = sessionKey;
            var ret = JoinSession(out var sessionHandle, SessionKey);
            if (ret != 0)
                throw new Win32Exception(ret);
            Handle = sessionHandle;
        }

        private IntPtr Handle { get; }
        public string SessionKey { get; }

        /// <inheritdoc />
        public void Dispose() {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        [DllImport("rstrtmgr", EntryPoint = "RmAddFilter", CharSet = CharSet.Auto)]
        private static extern int AddFilter(
            IntPtr sessionHandle,
            string fileName,
            UniqueProcess application,
            string serviceName,
            FilterAction filterAction);

        [DllImport("rstrtmgr", EntryPoint = "RmAddFilter", CharSet = CharSet.Auto)]
        private static extern int AddFilter(
            IntPtr sessionHandle,
            string fileName,
            IntPtr application,
            string serviceName,
            FilterAction filterAction);

        [DllImport("rstrtmgr", EntryPoint = "RmEndSession")]
        private static extern int EndSession(IntPtr sessionHandle);

        [DllImport("rstrtmgr", EntryPoint = "RmJoinSession", CharSet = CharSet.Auto)]
        private static extern int JoinSession(out IntPtr sessionHandle, string strSessionKey);

        [DllImport("rstrtmgr", EntryPoint = "RmRegisterResources", CharSet = CharSet.Auto)]
        private static extern int RegisterResources(
            IntPtr sessionHandle,
            uint numberOfFiles,
            string[] fileNames,
            uint numberOfApplications,
            UniqueProcess[] applications,
            uint numberOfServices,
            string[] serviceNames);

        [DllImport("rstrtmgr", EntryPoint = "RmRestart")]
        private static extern int Restart(IntPtr sessionHandle, int restartFlags,
            WriteStatusCallback statusCallback);

        [DllImport("rstrtmgr", EntryPoint = "RmShutdown")]
        private static extern int Shutdown(
            IntPtr sessionHandle,
            ShutdownType actionFlags,
            WriteStatusCallback statusCallback);

        [DllImport("rstrtmgr", EntryPoint = "RmStartSession", CharSet = CharSet.Auto)]
        private static extern int StartSession(out IntPtr sessionHandle, int sessionFlags,
            string strSessionKey);

        public void FilterProcess(Process process, FilterAction action) {
            var ret = AddFilter(Handle, null, UniqueProcess.FromProcess(process),
                null, action);
            if (ret != 0)
                throw new Win32Exception(ret);
        }

        public void FilterProcessFile(FileInfo file, FilterAction action) {
            var ret = AddFilter(Handle, file.FullName, IntPtr.Zero, null, action);
            if (ret != 0)
                throw new Win32Exception(ret);
        }

        public void FilterService(ServiceController service, FilterAction action) {
            var ret = AddFilter(Handle, null, IntPtr.Zero, service.ServiceName, action);
            if (ret != 0)
                throw new Win32Exception(ret);
        }

        public void RegisterProcess(params Process[] processes) {
            var ret = RegisterResources(Handle,
                0, new string[0],
                (uint)processes.Length, processes.Select(UniqueProcess.FromProcess).ToArray(),
                0, new string[0]);
            if (ret != 0)
                throw new Win32Exception(ret);
        }

        public void RegisterProcessFile(params FileInfo[] files) {
            var ret = RegisterResources(Handle,
                (uint)files.Length, files.Select(f => f.FullName).ToArray(),
                0, new UniqueProcess[0],
                0, new string[0]);
            if (ret != 0)
                throw new Win32Exception(ret);
        }

        public void RegisterService(params ServiceController[] services) {
            var ret = RegisterResources(Handle,
                0, new string[0],
                0, new UniqueProcess[0],
                (uint)services.Length, services.Select(s => s.ServiceName).ToArray());
            if (ret != 0)
                throw new Win32Exception(ret);
        }

        public void Restart(WriteStatusCallback statusCallback) {
            var ret = Restart(Handle, 0, statusCallback);
            if (ret != 0)
                throw new Win32Exception(ret);
        }

        public void Restart() {
            Restart(null);
        }

        public void Shutdown(ShutdownType shutdownType, WriteStatusCallback statusCallback) {
            var ret = Shutdown(Handle, shutdownType, statusCallback);
            if (ret != 0)
                throw new Win32Exception(ret);
        }

        public void Shutdown(ShutdownType shutdownType) {
            Shutdown(shutdownType, null);
        }

        private void ReleaseUnmanagedResources() {
            try {
                EndSession(Handle);
            } catch {
                // ignored
            }
        }

        /// <inheritdoc />
        ~RestartManagerSession() {
            ReleaseUnmanagedResources();
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct FileTime {
            private uint LowDateTime;
            private uint HighDateTime;

            public static FileTime FromDateTime(DateTime dateTime) {
                var ticks = dateTime.ToFileTime();

                return new FileTime {
                    HighDateTime = (uint)(ticks >> 32),
                    LowDateTime = (uint)(ticks & uint.MaxValue)
                };
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct UniqueProcess {
            private int ProcessId;
            private FileTime ProcessStartTime;

            public static UniqueProcess FromProcess(Process process) {
                return new UniqueProcess {
                    ProcessId = process.Id,
                    ProcessStartTime = FileTime.FromDateTime(process.StartTime)
                };
            }
        }
    }
}