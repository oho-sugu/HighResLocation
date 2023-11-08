#if UNITY_IOS && !UNITY_EDITOR
using System;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

namespace tech.orthoverse.location
{
    public class iOSLocation : IDisposable
    {
        private IntPtr _ptr;
        private bool IsDisposed => _ptr == IntPtr.Zero;

        public iOSLocation()
        {
            _ptr = CreateHighResLocation();
        }

        ~iOSLocation()
        {
            _dispose();
        }


        void IDisposable.Dispose()
        {
            _dispose();
            GC.SuppressFinalize(this);
        }

        private void _dispose()
        {
            var ptr = Interlocked.Exchange(ref _ptr, IntPtr.Zero);
            if (ptr != IntPtr.Zero)
            {
                ReleaseHighResLocation(ptr);
            }
        }

        public bool Start()
        {
            if (IsDisposed)
            {
                return false;
            }
            return StartLocation(_ptr) != 0;
        }

        public void Stop()
        {
            if (IsDisposed)
            {
                return;
            }
            StopLocation(_ptr);
        }

        public bool IsLocalized()
        {
            if (IsDisposed)
            {
                return false;
            }
            return Localized(_ptr) != 0;
        }

        public HRLocation GetLocation()
        {
            if (IsDisposed)
            {
                throw new Exception("HRLocation have disposed.");
            }
            var loc = new HRLocation();
            loc.longitude = GetLongitude(_ptr);
            loc.latitude = GetLatitude(_ptr);
            loc.altitude = GetAltitude(_ptr);
            loc.ellipsoidalHeight = GetEllipsoidalHeight(_ptr);

            return loc;
        }

        [DllImport("__Internal", EntryPoint = "createHighResLocation")]
        private static extern IntPtr CreateHighResLocation();

        [DllImport("__Internal", EntryPoint = "releaseHighResLocation")]
        private static extern void ReleaseHighResLocation(IntPtr highResLocation);
        
        [DllImport("__Internal", EntryPoint = "startLocation")]
        private static extern byte StartLocation(IntPtr highResLocation);

        [DllImport("__Internal", EntryPoint = "stopLocation")]
        private static extern void StopLocation(IntPtr highResLocation);

        [DllImport("__Internal", EntryPoint = "localized")]
        private static extern byte Localized(IntPtr highResLocation);

        [DllImport("__Internal", EntryPoint = "getLongitude")]
        private static extern double GetLongitude(IntPtr highResLocation);

        [DllImport("__Internal", EntryPoint = "getLatitude")]
        private static extern double GetLatitude(IntPtr highResLocation);

        [DllImport("__Internal", EntryPoint = "getAltitude")]
        private static extern double GetAltitude(IntPtr highResLocation);

        [DllImport("__Internal", EntryPoint = "getEllipsoidalHeight")]
        private static extern double GetEllipsoidalHeight(IntPtr highResLocation);
    }
}
#endif
