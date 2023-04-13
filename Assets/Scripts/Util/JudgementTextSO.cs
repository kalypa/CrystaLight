using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Sprite/JudgementText")]
public class JudgementTextSO : ScriptableObject
{
    public List<Sprite> accuracyList = new();
}