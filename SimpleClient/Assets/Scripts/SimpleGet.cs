using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SimpleGet : MonoBehaviour
{
    public Button btnGet;
    public Text text;

    private readonly string url = "http://localhost:54000";

    private void Start()
    {
        btnGet.onClick.AddListener(() => StartCoroutine(GetRequest()));
    }

    IEnumerator GetRequest()
    {
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();

        if(req.result == UnityWebRequest.Result.Success)
        {
            string data = req.downloadHandler.text;
            print(data);
            //GgmVO vo = JsonUtility.FromJson<GgmVO>(data);

            //string s = "";

            //s += vo.name + " " + vo.openYear + " ";
            //foreach (var item in vo.circle)
            //{
            //    s += item.name + " " + item.id + " ";
            //}


            //foreach (var item in vo.grade)
            //{
                
            //    print(item.name + item.grade + item.cid);
            //    s += item.name + " " + item.grade + " " +item.cid;

            //}



            //foreach (var item in vo.teacherList)
            //{
            //    s += item + " ";
            //}
            //text.text = s;
        }
        else
        {
            Debug.LogError("데이터 통신중 오류 발생");
        }
    }
}
