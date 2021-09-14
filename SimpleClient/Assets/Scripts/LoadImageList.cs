using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Linq;

public class LoadImageList : MonoBehaviour
{
    public Button loadBtn;
    public Transform listContent;
    public GameObject listUIPrefab;
    public Image loadedImage;

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
                obj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    loadedImage.sprite = obj.GetComponentsInChildren<Image>()[1].sprite;
                });
                ListItem li = obj.GetComponent<ListItem>();

                StartCoroutine(LoadThumbnail(li, item));
            }
        }
    }

    IEnumerator LoadThumbnail(ListItem li, string filename)
    {
        UnityWebRequest req = UnityWebRequestTexture.GetTexture($"http://localhost:54000/thumb?file=r_{filename}");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Texture2D t = (req.downloadHandler as DownloadHandlerTexture).texture;

            Rect rect = new Rect(0, 0, t.width, t.height);
            Sprite s = Sprite.Create(t, rect, new Vector2(0.5f, 0.5f));

            li.SetData(filename, s);

        }
        
    }

    
}
