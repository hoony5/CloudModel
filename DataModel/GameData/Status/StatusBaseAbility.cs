[System.Serializable]
public class StatusBaseAbility
{
    public string Name { get; private set; }
    public List<StatusItemInfo> StatusItems { get; private set; }
    public StatusBaseAbility(){}
    public StatusBaseAbility(string name) => Name = name;
    public void SetStatusesBaseInfo(List<StatusItemInfo> list)
    {
        StatusItems = list;
    }
    
    public void Clear()
    {
        StatusItems.Clear();
    }
    protected void ClearValues()
    {
        for (var index = 0; index < StatusItems.Count; index++)
        {
            StatusItems[index].SetValue(0);
        }
    }

    public List<StatusItemInfo> GetStatuses()
    {
        return StatusItems;
    }

    public void SetBaseValue(string statusName, float value)
    {
        foreach (StatusItemInfo stat in StatusItems)
        {
            if (stat.RawName.Equals(statusName))
            {
                stat.SetValue(value);
            }
        }
    }
    public void AddBaseValue(string statusName, float value)
    {
        foreach (StatusItemInfo stat in StatusItems)
        {
            if (stat.RawName.Equals(statusName))
            {
                stat.AddValue(value);
            }
        }
    }
    public void MultiplyBaseValue(string statusName, float value)
    {
        foreach (StatusItemInfo stat in StatusItems)
        {
            if (stat.RawName.Equals(statusName))
            {
                stat.MultiplyValue(value);
            }
        }
    }
}
