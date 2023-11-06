using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public static Location Instance { set; get; }

    public bool getFlag = false;
    public float latitude;
    public float longitude;
    public float altitude;

    public string error = "";
    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
 //       StartCoroutine(StartLocationService());
    }

    public IEnumerator StartLocationService()
    {
        getFlag = false;
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            error = "GPS not enabled";
            Debug.Log("GPS not enabled");
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait <= 0)
        {
            error = "Timed out";
            Debug.Log("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            error = "Unable to determine device location";
            Debug.Log("Unable to determine device location");
            yield break;
        }

        // Set locational infomations
        while (true)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            altitude = Input.location.lastData.altitude;
            yield return new WaitForSeconds(10);
        }

        getFlag = true;
    }
}
