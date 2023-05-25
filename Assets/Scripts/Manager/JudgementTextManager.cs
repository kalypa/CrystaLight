using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementTextManager : MonoBehaviour
{
    public static JudgementTextManager Instance;
    private void Awake() => Instance = this;
    public JudgementTextSO judgementTextSO;

    public void JudgementTextDataInit(List<Sprite> s)
    {
        foreach (Sprite sp in judgementTextSO.accuracyList) s.Add(sp);
    }
}
