using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using DG.Tweening;

public class LoadScoreList : MonoBehaviour
{
    public Button loadBtn;
    public Transform contentView;
    public GameObject rankItemPrefab;


    private void Start()
    {
        loadBtn.onClick.AddListener(() =>
        {
            NetworkManager.instance.SendGetRequest("list","",ListGenerate);
        });

    }
    private void ListGenerate(string json)
    {
        Transform[] childs = contentView.GetComponentsInChildren<Transform>();
        ResponseVO res = JsonUtility.FromJson<ResponseVO>(json);

        if(res.result)
        {
            LoadScoreVO vo = JsonUtility.FromJson<LoadScoreVO>(res.payload);
            for (int i = 1; i < childs.Length; i++)
            {
                Destroy(childs[i].gameObject);
            }

            for (int i = 0; i < vo.list.Count; i++)
            {
                GameObject obj = Instantiate(rankItemPrefab, contentView);
                RankItem rankItem = obj.GetComponent<RankItem>();
                rankItem.SetData(i + 1, vo.list[i]);
                rankItem.SetAnimation(i * 0.4f + 0.1f);
            }
        }
        else
        {

        }
    }
}
