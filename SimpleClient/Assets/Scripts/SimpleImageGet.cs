using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SimpleImageGet : MonoBehaviour
{
    public Button btnImageGet;
    public Image loadedImage;

    private readonly string url = "http://localhost:52000/image";

    private void Start()
    {
        btnImageGet.onClick.AddListener(() => StartCoroutine(ImageRequest()));
    }

    IEnumerator ImageRequest()
    {
        UnityWebRequest req = UnityWebRequestTexture.GetTexture(url);

        yield return req.SendWebRequest();

    }
}
