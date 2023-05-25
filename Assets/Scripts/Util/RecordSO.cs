using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SaveData/RecordSO")]
public class RecordSO : ScriptableObject
{
    public List<int> scoreText = new();
    public List<string> ratingText = new();
    public List<int> maxComboText = new();
    public List<float> accuracyText = new();
    public List<int> clearText = new();
}
