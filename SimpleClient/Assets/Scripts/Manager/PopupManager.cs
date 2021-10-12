using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopupManager : MonoBehaviour
{
    public Button registerPopupBtn;
    public Button loginPopupBtn;
    public Transform popupParent;

    private CanvasGroup popupCanvasGroup;

    public RegisterPopup registerPopupPrefab;
    public AlertPopup alertPopupPrefab;
    public LoginPopup loginPopupPrefab;

    private Dictionary<string, Popup> popupDic = new Dictionary<string, Popup>();
    private Stack<Popup> popupStack = new Stack<Popup>();

    public static PopupManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("다수의 팝업매니저가 실행중");
        }
        instance = this;
    }

    private void Start()
    {
        popupCanvasGroup = popupParent.GetComponent<CanvasGroup>();
        if(popupCanvasGroup == null)
        {
            popupCanvasGroup = popupParent.gameObject.AddComponent<CanvasGroup>();
        }
        popupCanvasGroup.alpha = 0;
        popupCanvasGroup.interactable = false;
        popupCanvasGroup.blocksRaycasts = false;

        popupDic.Add("register", Instantiate(registerPopupPrefab, popupParent));
        popupDic.Add("login", Instantiate(loginPopupPrefab, popupParent));
        popupDic.Add("alert", Instantiate(alertPopupPrefab, popupParent));

        registerPopupBtn.onClick.AddListener(() => OpenPopup("register"));
        loginPopupBtn.onClick.AddListener(() => OpenPopup("login"));
    }

    public void OpenPopup(string name,object data = null, int closeCount = 1)
    {
        //최초로 열리는 팝업
        if(popupStack.Count == 0)
        {
            popupCanvasGroup.interactable = true;
            DOTween.To(() => popupCanvasGroup.alpha, value => popupCanvasGroup.alpha = value, 1, 0.8f).OnComplete(() =>
            {
                popupCanvasGroup.interactable = true;
                popupCanvasGroup.blocksRaycasts = true;
            });
            
        }
        popupStack.Push(popupDic[name]);
        popupDic[name].Open(data,closeCount);
    }

    public void ClosePopup()
    {
        popupStack.Pop().Close();
        if(popupStack.Count == 0)
        {
            DOTween.To(() => popupCanvasGroup.alpha, value => popupCanvasGroup.alpha = value, 0, 0.8f).OnComplete(() =>
            {
                popupCanvasGroup.interactable = false;
                popupCanvasGroup.blocksRaycasts = false;
            });
        }

        
    }
}
