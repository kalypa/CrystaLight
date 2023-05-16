using System.Collections.Generic;

public class OffsetManager : SingleMonobehaviour<OffsetManager>
{
    public List<double> offsetList = new List<double>();

    public double GetOffset()
    {
        double sum = 0;
        foreach (double offset in offsetList) sum += offset;
        return sum / offsetList.Count;
    }
}
