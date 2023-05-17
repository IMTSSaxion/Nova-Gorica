using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GatherData : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    private DateTime dateTime;
    private Vector2 geoLocation;

    public static GatherData Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Another instance of this class has been found", this);
    }

    public void GetAllData()
    {
        StartCoroutine(GetLocation());
        GetDateTime();
    }

    public void GetDateTime()
    {
        dateTime = DateTime.Now;
        Debug.Log("dateTime: " + dateTime);
    }

    public IEnumerator GetLocation()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogWarning("GPS is not enabled");
            text.text = "GPS is not enabled";
            yield break;
        }

        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            Debug.LogWarning("Timed out");
            text.text = "Timed out";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogWarning("Unable to determine device location");
            text.text = "Unable to determine device location";
            yield break;
        }
        else
        {
            Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " +
                Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            geoLocation.x = Input.location.lastData.longitude;
            geoLocation.y = Input.location.lastData.latitude;
            text.text = $"Longitude: {geoLocation.x}\nLatitude: {geoLocation.y}";
        }
    }

    /// <summary>
    /// Saves gathered data on a local file
    /// </summary>
    private void SaveDataLocal()
    {

    }
}
