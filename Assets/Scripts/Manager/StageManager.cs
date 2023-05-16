using System.Collections.Generic;
using TMPro;
public class StageManager : SingleMonobehaviour<StageManager>
{
    public List<StageSO> stageList;
    public int currentStageNum = 0;
    public TMP_Text songName;
    public TMP_Text artistName;
}
