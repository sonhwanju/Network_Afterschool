using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Linq;
using System;

public class LoadImageList : MonoBehaviour
{
    public Button loadBtn;
    public Transform listContent;
    public GameObject listUIPrefab;
    public Image imageField;

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

        if (req.result == UnityWebRequest.Result.Success)
        {
            string json = req.downloadHandler.text;
            ImageListVO vo = JsonUtility.FromJson<ImageListVO>(json);

            print(vo.msg);

            Transform[] childs = listContent.GetComponentsInChildren<Transform>();
            for (int i = 1; i < childs.Length; i++)
            {
                Destroy(childs[i].gameObject);
            }
            //listContent.GetComponentsInChildren<Transform>().ToList().ForEach(x => Destroy(x.gameObject));
            foreach (var item in vo.list)
            {
                GameObject obj = Instantiate(listUIPrefab, listContent);
                ListItem li = obj.GetComponent<ListItem>();

                LoadThumbnail(li, item);
            }
        }
    }

    public void LoadThumbnail(ListItem li, string filename)
    {
        string url = $"http://localhost:54000/thumb?file=r_{filename}";
        StartCoroutine(GetImageFromServer(url, s => li.SetData(filename, s)));

        li.btnLoad.onClick.AddListener(() =>
        {
            string origin = $"http://localhost:54000/image?file={filename}";
            StartCoroutine(GetImageFromServer(origin, s => imageField.sprite = s));

        });
    }
    IEnumerator GetImageFromServer(string url, Action<Sprite> handler)
    {
        UnityWebRequest req = UnityWebRequestTexture.GetTexture(url);

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Texture2D t = (req.downloadHandler as DownloadHandlerTexture).texture;

            Rect rect = new Rect(0, 0, t.width, t.height);
            Sprite s = Sprite.Create(t, rect, new Vector2(0.5f, 0.5f));

            handler(s);
        }
    }

}
