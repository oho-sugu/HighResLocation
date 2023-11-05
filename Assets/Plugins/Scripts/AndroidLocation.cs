#if UNITY_ANDROID && !UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using tech.orthoverse.location;
using UnityEngine;

public class AndroidLocation
{
    private AndroidJavaObject locationAdapter;
    private AndroidJavaObject activity;

    public void init()
    {
        var player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        activity = player.GetStatic<AndroidJavaObject>("currentActivity");

        locationAdapter = new AndroidJavaObject("tech.orthoverse.location.HighResLocation");
        locationAdapter.Call("init", activity);
    }

    public void stop()
    {
        locationAdapter.Call("stop");
    }

    public bool isLocalized()
    {
        return locationAdapter.Call<bool>("isLocalized");
    }

    public HRLocation getLocation()
    {
        var location = new HRLocation();
        location.latitude = locationAdapter.Call<double>("getLatitude");
        location.longitude = locationAdapter.Call<double>("getLongitude");
        location.altitude = locationAdapter.Call<double>("getAltitude");
        location.ellipsoidalHeight = locationAdapter.Call<double>("getEllipsoidalHeight");

        return location;
    }
}

#endif