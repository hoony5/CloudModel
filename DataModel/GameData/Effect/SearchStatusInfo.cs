[System.Serializable]
public class SearchStatusInfo
{
    public string EffectName { get; private set; }
    public List<SearchStatusItem> Items { get; private set; }
    
    public SearchStatusInfo(string effectName, List<SearchStatusItem> items)
    {
        EffectName = effectName;
        Items = items;
    }
}
