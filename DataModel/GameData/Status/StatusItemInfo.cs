[System.Serializable]
public class StatusItemInfo 
{
    public string RawName { get; set; }
    public string DisplayName { get; set; }
    public float Value { get; private set; }
    public int Index { get; set; }
    public StatusItemInfo(){ }
    public StatusItemInfo(string rawName, string displayName, int index)
    {
        RawName = rawName;
        DisplayName = displayName;
        Value = 0;
        Index = index;
    }

    public static readonly StatusItemInfo Empty = new StatusItemInfo {
        RawName = "Empty",
        DisplayName = "Empty",
        Value = 0,
        Index = -1,
    };
    
    public void MultiplyValue(float value)
    {
        Value *= value;
    }
    public void AddValue(float value)
    {
        Value += value;
    }
    public void SetValue(float value)
    {
        Value = value;
    }
}