using System.Collections;
using System.Collections.Generic;
using tech.orthoverse.location;
using UnityEngine;
using UnityEngine.UI;

public class TestLocation : MonoBehaviour
{
    private HighResLocation location = new HighResLocation();

    public Text latitude, longitude, altitude, ellipsoidalHeight;

    // Start is called before the first frame update
    void Start()
    {
        location.StartLocation();
    }

    private float timer = 0f;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1f)
        {
            timer = 0f;
            var loc = location.GetLocation();

            latitude.text = loc.latitude.ToString();
            longitude.text = loc.longitude.ToString();
            altitude.text = loc.altitude.ToString();
            ellipsoidalHeight.text = loc.ellipsoidalHeight.ToString();
        }
        
    }
}
