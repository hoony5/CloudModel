[System.Serializable]
public class StatusItemInfo 
{
    public string RawName { get; set; }
    public string DisplayName { get; set; }
    public float Value { get; private set; }
    public int Index { get; set; }
    public StatusItemInfo(string rawName, string displayName, int index)
    {
        RawName = rawName;
        DisplayName = displayName;
        Value = 0;
        Index = index;
    }
}