using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;

public class SimpleImageGet : MonoBehaviour
{
    public Button btnImageGet;
    public Button btnImageGet2;
    public Image loadedImage;

    private readonly string url = "http://localhost:54000/image";
    private readonly string url2 = "http://localhost:54000/image2";

    private void Start()
    {
        btnImageGet.onClick.AddListener(() => StartCoroutine(ImageRequest()));
        btnImageGet2.onClick.AddListener(() => StartCoroutine(ImageRequest2()));
    }

    IEnumerator ImageRequest()
    {
        string path = Application.persistentDataPath + "/youandme.png";

        UnityWebRequest req = UnityWebRequestTexture.GetTexture(url);

        yield return req.SendWebRequest();

        if (File.Exists(path))
        {
            byte[] byteData = File.ReadAllBytes(path);
            Texture2D t;

            t = new Texture2D(0,0);
            t.LoadImage(byteData);

            Rect rect = new Rect(0, 0, t.width, t.height);
            Sprite s = Sprite.Create(t, rect, new Vector2(0.5f, 0.5f));

            loadedImage.sprite = s;
            print(path);
            print("불러옴");
        }
        else
        {
            if (req.result == UnityWebRequest.Result.Success)
            {
                Texture2D t = (req.downloadHandler as DownloadHandlerTexture).texture;

                Rect rect = new Rect(0, 0, t.width, t.height);
                Sprite s = Sprite.Create(t, rect, new Vector2(0.5f, 0.5f));
                loadedImage.sprite = s;

                byte[] byteData = t.EncodeToPNG();
                File.WriteAllBytes(path, byteData);
                print("가져옴");
            }
            else
            {
                Debug.LogError("데이터통신중 오류발생");
            }
        }
        //이 path의 파일이 존재하는지를 체크해서  존재하지 않으면 요청
        //존재하면 해당파일을 불러와서 이미지로드

        

    }
    IEnumerator ImageRequest2()
    {
        //string a =Directory.GetCurrentDirectory() + "/Assets/nyan.jpg";
        string path = Application.persistentDataPath + "/nyan.jpg";

        UnityWebRequest req = UnityWebRequestTexture.GetTexture(url2);
        yield return req.SendWebRequest();

        if (File.Exists(path))
        {
            byte[] byteData = File.ReadAllBytes(path);
            Texture2D t;

            t = new Texture2D(0, 0);
            t.LoadImage(byteData);

            Rect rect = new Rect(0, 0, t.width, t.height);
            Sprite s = Sprite.Create(t, rect, new Vector2(0.5f, 0.5f));

            loadedImage.sprite = s;
            print("불러옴");
        }
        else
        {
            if (req.result == UnityWebRequest.Result.Success)
            {
                Texture2D t = (req.downloadHandler as DownloadHandlerTexture).texture;

                Rect rect = new Rect(0, 0, t.width, t.height);
                Sprite s = Sprite.Create(t, rect, new Vector2(0.5f, 0.5f));
                loadedImage.sprite = s;

                byte[] byteData = t.EncodeToJPG();
                File.WriteAllBytes(path, byteData);
                print("가졍ㅎㅁ");
            }
            else
            {
                Debug.LogError("데이터통신중 오류발생");
            }
        }
       

    }
}
