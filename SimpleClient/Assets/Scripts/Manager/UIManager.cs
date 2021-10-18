using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public RectTransform box1,box2,loginPanel;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("다수의 UI매니저가 실행중");
            return;
        }
        instance = this;
        //ShowBox2();
    }

    public void ShowBox2()
    {
        //loginPanel.sizeDelta.y;
        float lo = loginPanel.sizeDelta.y;
        box2.DOLocalMoveY(0, 1f);
        box1.DOLocalMoveY(lo, 1f);
    }
    public void ShowBox1()
    {
        float lo = loginPanel.sizeDelta.y;
        box1.DOLocalMoveY(0, 1f);
        box2.DOLocalMoveY(-lo, 1f);
    }
}
