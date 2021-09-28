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
        //����Ʈ raw �����ͷ� json�� ��ȯ�ؼ� �ѹ� �� ž������� �����۵�

        yield return req.SendWebRequest(); //������ ��û ����
        //������ ������ ������ ���� ������� ����

        if (req.result == UnityWebRequest.Result.Success)
        {
            string data = req.downloadHandler.text;
            print(data);
        }
        else
        {
            Debug.LogError("������ ����� ���� �߻�");
        }
    }
}
    