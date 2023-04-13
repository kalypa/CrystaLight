using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Collection/NotesSO")]
public class NotesSO : ScriptableObject
{
    public List<double> timeStamps = new();
    public List<double> endTimeStamps = new();
    public List<double> sameTimeInSideLaneList = new();
    public List<double> sameTimeInRefractionLaneList = new();
    public List<double> sameTimeInCrossLaneList = new();
    public List<long> noteLengthList = new();
}
