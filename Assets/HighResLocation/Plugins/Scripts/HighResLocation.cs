using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tech.orthoverse.location
{
    public class HighResLocation
    {
#if UNITY_IOS && !UNITY_EDITOR
        private iOSLocation _location;
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
        private AndroidLocation _location;
#endif
        public void StartLocation()
        {
#if UNITY_IOS && !UNITY_EDITOR
            _location = new iOSLocation();
            _location.Start();
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
            _location = new AndroidLocation();
            _location.init();
#endif
    }

    public void StopLocation()
        {
#if UNITY_IOS && !UNITY_EDITOR
            _location.Stop();
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
            _location.stop();
#endif
        }

        public HRLocation GetLocation()
        {
#if UNITY_IOS && !UNITY_EDITOR
            if (_location.IsLocalized())
            {
                return _location.GetLocation();
            }
            throw new System.Exception("not localized");
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
            if (_location.isLocalized())
            {
                return _location.getLocation();
            }
            throw new System.Exception("not localized");
#endif
            throw new System.Exception("Editor cant get location");
        }
    }

    public struct HRLocation
    {
        public double longitude;
        public double latitude;
        public double altitude;
        public double ellipsoidalHeight;
    }
}
