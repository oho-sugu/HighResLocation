import CoreLocation

public class HighResLocation {
    private var longitude : Double
    private var latitude : Double
    private var altitude : Double
    private var ellipsoidalHeight : Double

    private var isLocalized : Bool

    private var locationManager : CLLocationManager!

    public func startLocation() -> Bool {
        locationManager = CLLocationManager()

        guard let locationManager = locationManager else { return false }
        locationManager.requestWhenInUseAuthorization()

        if CLLocationManager.locationServicesEnabled() {
            locationManager.delegate = self
            locationManager.desiredAccuracy = kCLLocationAccuracyBest
            locationManager.distanceFilter = kCLDistanceFilterNone
            locationManager.startUpdatingLocation()

            return true
        } else {
            return false
        }
    }

    public func stopLocation(){
        guard let locationManager = locationManager else { return }

        if CLLocationManager.locationServicesEnabled() {
            locationManager.stopUpdatingLocation()
            locationManager.delegate = nil
        }
    }

    public func Localized() -> Bool {
        return self.isLocalized
    }

    public func getLongitude() -> Double {
        return self.longitude
    }

    public func getLatitude() -> Double {
        return self.latitude
    }

    public func getAltitude() -> Double {
        return self.altitude
    }

    public func getEllipsoidalHeight() -> Double {
        return self.ellipsoidalHeight
    }
}

extension HighResLocation: CLLocationManagerDelegate {
    func locationManager(_ manager: CLLocationManager, didUpdateLocations locations: [CLLocation]){
        guard let newLocation = locations.last else { return }
        self.longitude = newLocation.coordinate.longitude
        self.latitude = newLocation.coordinate.latitude
        self.altitude = newLocation.altitude
        self.ellipsoidalHeight = newLocation.ellipsoidalAltitude

        self.isLocalized = true
    }
}

@_cdecl("createHighResLocation")
public func createHighResLocation() -> UnsafeRawPointer {
    let instance = HighResLocation()
    return UnsafeRawPointer(Unmanaged<HighResLocation>.passRetained(instance).toOpaque())
}

@_cdecl("releaseHighResLocation")
public func releaseHighResLocation(_ highResLocation: UnsafeRawPointer){
    Unmanaged<HighResLocation>.fromOpaque(highResLocation).release()
}

@_cdecl("startLocation")
public func startLocation(_ highResLocation: UnsafeRawPointer) -> UInt8 {
    let instance = Unmanaged<HighResLocation>.fromOpaque(highResLocation).takeUnretainedValue()
    return instance.startLocation() ? 1 : 0
}

@_cdecl("stopLocation")
public func stopLocation(_ highResLocation: UnsafeRawPointer){
    let instance = Unmanaged<HighResLocation>.fromOpaque(highResLocation).takeUnretainedValue()
    instance.stopLocation()
}

@_cdecl("localized")
public func localized(_ highResLocation: UnsafeRawPointer) -> UInt8 {
    let instance = Unmanaged<HighResLocation>.fromOpaque(highResLocation).takeUnretainedValue()
    return instance.Localized() ? 1 : 0
}

@_cdecl("getLongitude")
public func getLongitude(_ highResLocation: UnsafeRawPointer) -> Double {
    let instance = Unmanaged<HighResLocation>.fromOpaque(highResLocation).takeUnretainedValue()
    return instance.getLongitude()
}

@_cdecl("getLatitude")
public func getLatitude(_ highResLocation: UnsafeRawPointer) -> Double {
    let instance = Unmanaged<HighResLocation>.fromOpaque(highResLocation).takeUnretainedValue()
    return instance.getLatitude()
}

@_cdecl("getAltitude")
public func getAltitude(_ highResLocation: UnsafeRawPointer) -> Double {
    let instance = Unmanaged<HighResLocation>.fromOpaque(highResLocation).takeUnretainedValue()
    return instance.getAltitude()
}

@_cdecl("getEllipsoidalHeight")
public func getEllipsoidalHeight(_ highResLocation: UnsafeRawPointer) -> Double {
    let instance = Unmanaged<HighResLocation>.fromOpaque(highResLocation).takeUnretainedValue()
    return instance.getEllipsoidalHeight()
}
