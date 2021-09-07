using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SimpleGet : MonoBehaviour
{
    public Button btnGet;

    private readonly string url = "http://localhost:52000";

    private void Start()
    {
        btnGet.onClick.AddListener(() => StartCoroutine(GetRequest()));
    }

    IEnumerator GetRequest()
    {
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();
    }
}
