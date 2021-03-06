using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPopup : Popup
{
    public Button registerBtn;
    public Button closeBtn;

    public InputField nameInput;
    public InputField idInput;
    public InputField passInput;
    public InputField passConfirmInput;

    protected override void Awake()
    {
        base.Awake();
        registerBtn.onClick.AddListener(() =>
        {
            //passInput과 passConfirm이 다르면 debug.logerror 띄우고 => popup으로 교체  

            //if (nameInput.text.Trim() == "" || idInput.text.Trim() == "" || passInput.text.Trim() == "" || passConfirmInput.text.Trim() == "")
            //{
            //    return;
            //}
            Regex reg = new Regex(@"^[가-힣a-zA-Z]{2,3}$");
            if (!reg.IsMatch(nameInput.text))
            {
                Debug.Log("이름은 반드시 한글 또는 영문으로 2-3글자여야 합니다");
                PopupManager.instance.OpenPopup("alert", "이름은 반드시 한글 또는 영문으로 2-3글자여야 합니다");
                return;
            }

            RegisterVO vo = new RegisterVO(nameInput.text, idInput.text, passInput.text);
            string json = JsonUtility.ToJson(vo);
            NetworkManager.instance.SendPostRequest("register", json, result =>
             {
                 Debug.Log(result);
                 ResponseVO vo = JsonUtility.FromJson<ResponseVO>(result);
                 print(vo.result);

                 if(vo.result)
                 {
                     //회원가입창도 같이 닫히게
                     PopupManager.instance.OpenPopup("alert", vo.payload, 2);
                 }
                 else
                 {
                     //false라면 얼럿만 닫히게
                     PopupManager.instance.OpenPopup("alert", vo.payload);
                 }
             });
        });
        closeBtn.onClick.AddListener(() => { PopupManager.instance.ClosePopup(); });
    }
}  
