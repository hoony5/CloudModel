[System.Serializable]
public class SearchStatusItem
{
    public StatusItemInfo StatusItemInfo { get; private set; }
    public DataUnitType  SearchUnitType { get; private set; }
    public bool IsMeetCondition { get; private set; }

    public SearchStatusItem(StatusItemInfo statusItemInfo, DataUnitType searchUnitType)
    {
        StatusItemInfo = statusItemInfo;
        SearchUnitType = searchUnitType;
    }
}