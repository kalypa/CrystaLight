using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public List<string> scoreText = new();
    public List<string> ratingText = new();
    public List<string> maxComboText = new();
    public List<string> accuracyText = new();
    public List<string> clearText = new();

}
public class DataManager : SingleMonobehaviour<DataManager>
{
    string path;

    void Start()
    {
        path = Path.Combine(Application.dataPath + "/SaveData/", "database.json");
        JsonLoad();
    }

    public void JsonLoad()
    {
        SaveData saveData = new SaveData();

        if (!File.Exists(path))
        {
            JsonSave();
        }

        else
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);
            if (saveData != null)
            {
                RepeatSaveData(GameManager.Instance.recordSO.scoreText, saveData.scoreText);
                RepeatSaveDataString(GameManager.Instance.recordSO.ratingText, saveData.ratingText);
                RepeatSaveData(GameManager.Instance.recordSO.maxComboText, saveData.maxComboText);
                RepeatSaveDataFloat(GameManager.Instance.recordSO.accuracyText, saveData.accuracyText);
                RepeatSaveData(GameManager.Instance.recordSO.clearText, saveData.clearText);
            }
        }
    }

    public void JsonSave()
    {
        SaveData saveData = new SaveData();

        RepeatSaveData(GameManager.Instance.recordSO.scoreText, saveData.scoreText);
        RepeatSaveDataString(GameManager.Instance.recordSO.ratingText, saveData.ratingText);
        RepeatSaveData(GameManager.Instance.recordSO.maxComboText, saveData.maxComboText);
        RepeatSaveDataFloat(GameManager.Instance.recordSO.accuracyText, saveData.accuracyText);
        RepeatSaveData(GameManager.Instance.recordSO.clearText, saveData.clearText);
        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, json);
    }

    void RepeatSaveData(List<int> list, List<string> list2)
    {
        for (int i = 0; i < list.Count; i++) list2.Add(list[i].ToString());
    }
    void RepeatSaveDataFloat(List<float> list, List<string> list2)
    {
        for (int i = 0; i < list.Count; i++) list2.Add(list[i].ToString());
    }
    void RepeatSaveDataString(List<string> list, List<string> list2)
    {
        for (int i = 0; i < list.Count; i++) list2.Add(list[i].ToString());
    }
}
