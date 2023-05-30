[Serializable]
public class CloudDataModel
{
    public MessageInfo MessageInfo { get; private set; }
    public IdentifyInfo IdentifyInfo { get; private set; }
    public List<RowData> RowDataList { get; private set; }
    
    public CloudDataModel(IdentifyInfo identifyInfo, List<RowData> rowDataList, MessageInfo messageInfo)
    {
        IdentifyInfo = identifyInfo;
        RowDataList = rowDataList;
        MessageInfo = messageInfo;
    }
}