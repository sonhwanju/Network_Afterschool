using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RankItem : MonoBehaviour
{
    public Text rankText;
    public Text nameText;
    public Text scoreText;
    public Text msgText;

    public void SetAnimation(float delay)
    {
        transform.localScale = new Vector3(1f, 0f, 1f);
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(delay);
        seq.Append(transform.DOScaleY(1f, 0.4f));
    }

    public void SetData(int rank, RecordVO vo)
    {
        rankText.text = rank.ToString();
        nameText.text = vo.name;
        scoreText.text = vo.score.ToString();
        msgText.text = vo.msg;
    }
}
