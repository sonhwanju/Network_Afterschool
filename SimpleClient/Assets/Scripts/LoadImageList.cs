using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadImageList : MonoBehaviour
{
    public Button loadBtn;
    public Transform contentTrm;
    public GameObject panelPrefab;

    private readonly string url = "http://localhost:54000/fileList";


    private void Start()
    {
        loadBtn.onClick.AddListener(() =>
        {
            StartCoroutine(GetImageList());
        });
    }
    IEnumerator GetImageList()
    {
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();

        if(req.result == UnityWebRequest.Result.Success)
        {
            string json = req.downloadHandler.text;
            ImageListVO vo = JsonUtility.FromJson<ImageListVO>(json);

            print(vo.msg);

            foreach (var item in vo.list)
            {
                GameObject obj = Instantiate(panelPrefab, contentTrm);
                obj.GetComponentInChildren<Text>().text = item;
            }
        }
    }
}
