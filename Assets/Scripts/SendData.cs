using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendData : MonoBehaviour
{
    public IEnumerator SendDate()
    {
        WWWForm form = new WWWForm();

        form.AddField("DateTime", PlayerPrefs.GetString("DateTime"));
        form.AddField("Lat", PlayerPrefs.GetString("GeoLocX"));
        form.AddField("Long", PlayerPrefs.GetString("GeoLocY"));

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/", form))
        {
            Debug.Log("Attempting to send data");
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError(www.error);
            else
                Debug.Log("Form upload complete!");
        }
    }
}
