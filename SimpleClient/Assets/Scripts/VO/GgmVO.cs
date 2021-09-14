using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GgmVO
{
    public string name;
    public int openYear;
    public List<CircleVO> circle;
    public List<CircleGradeVO> grade = new List<CircleGradeVO>();
    public List<string> teacherList;
}
