using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GgmVO
{
    public string name;
    public int openYear;
    public List<CircleVO> circle;
    public List<CircleGrade2VO> grade = new List<CircleGrade2VO>();
    public List<string> teacherList;
}
