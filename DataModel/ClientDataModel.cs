[Serializable]
public class ClientDataModel
{
    public MessageInfo MessageInfo { get; private set; }
    public List<RowData> RowDataList { get; private set; }
    
    public ClientDataModel(List<RowData> rowDataList, MessageInfo messageInfo)
    {
        RowDataList = rowDataList;
        MessageInfo = messageInfo;
    }
}