using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PostData : MonoBehaviour
{
    public InputField txtName;
    public InputField txtMsg;
    public InputField txtScore;

    public Button btnSend;

    private readonly string url = "http://localhost:54000/postdata";

    private void Start()
    {   
        btnSend.onClick.AddListener(() =>
        {
            StartCoroutine(SendDataPost(txtName.text,txtMsg.text,txtScore.text));
        });
    }

    IEnumerator SendDataPost(string name, string msg, string score)
    {
        RecordVO vo = new RecordVO(name, msg, score);
        string json = JsonUtility.ToJson(vo);

        UnityWebRequest req = UnityWebRequest.Post(url, json);
        req.SetRequestHeader("Content-Type", "application/json");

        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        req.uploadHandler = new UploadHandlerRaw(jsonToSend);
        //바이트 raw 데이터로 json을 변환해서 한번 더 탑재해줘야 정상작동

        yield return req.SendWebRequest(); //실제로 요청 전송
        //전송이 끝나고 응답이 오면 여기부터 실행

        if (req.result == UnityWebRequest.Result.Success)
        {
            string data = req.downloadHandler.text;
            print(data);
        }
        else
        {
            Debug.LogError("데이터 통신중 오류 발생");
        }
    }
}
    