package tech.orthoverse.location

import android.content.Context
import android.location.LocationManager
import android.location.LocationListener
import android.location.Location
import android.os.Bundle


class HighResLocation() : LocationListener {
    private lateinit var locationManager: LocationManager

    private lateinit var lastLocation: Location
    private var localized = false

    fun init(context: Context){
        locationManager = context.getSystemService(Context.LOCATION_SERVICE) as LocationManager
        locationManager.requestLocationUpdates(
            LocationManager.GPS_PROVIDER,
            500,
            0f,
            this)
        locationManager.requestLocationUpdates(
            LocationManager.NETWORK_PROVIDER,
            500,
            0f,
            this)
        locationManager.requestLocationUpdates(
            LocationManager.PASSIVE_PROVIDER,
            500,
            0f,
            this)
    }

    fun stop(){
        locationManager.removeUpdates(this)
    }

    fun isLocalized(): Boolean {
        return localized
    }

    fun getLatitude(): Double {
        return lastLocation.getLatitude()
    }
    
    fun getLongitude(): Double {
        return lastLocation.getLongitude()
    }

    fun getAltitude(): Double {
        //return lastLocation.getMslAltitudeMeters()
        return 0.0;
    }

    fun getEllipsoidalHeight(): Double {
        return lastLocation.getAltitude()
    }

    override fun onLocationChanged(location: Location) {
        lastLocation = location
        localized = true
    }

    override fun onStatusChanged(provider: String, status: Int, extras: Bundle) { }
    override fun onProviderEnabled(provider: String) { }
    override fun onProviderDisabled(provider: String) { }
}