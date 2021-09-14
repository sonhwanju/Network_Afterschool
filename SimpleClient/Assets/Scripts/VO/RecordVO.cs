using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using UnityEngine;

[System.Serializable]
public class RecordVO
{
    public string name;
    public string msg;
    public int score;

    public RecordVO(string name, string msg, string score)
    {
        this.name = name;
        this.msg = msg;
        this.score = int.Parse(score);
    }

    public override string ToString()
    {
        return $"?name={EncodeStr(name)}&msg={EncodeStr(msg)}&score={score}";
    }

    private string EncodeStr(string str)
    {
        return HttpUtility.UrlEncode(str, Encoding.UTF8);
    }
}
