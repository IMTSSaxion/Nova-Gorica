using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

[RequireComponent(typeof(DefaultObserverEventHandler))]
public class ScanPicture : MonoBehaviour
{
    public void HandlePictureScan()
    {
        Debug.Log("Sending data to database");
        GatherData.Instance.GetAllData();
    }
}
