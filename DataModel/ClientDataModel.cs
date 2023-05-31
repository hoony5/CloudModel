using CloudModel.DataModel.Raw;
namespace CloudModel.DataModel;
[Serializable]
public class ClientDataModel
{
    public MessageInfo MessageInfo { get; private set; }
    public List<RowData> RowDataList { get; private set; }

    private bool isCreated;
    
    public ClientDataModel(List<RowData> rowDataList, MessageInfo messageInfo)
    {
        RowDataList = rowDataList;
        MessageInfo = messageInfo;
        isCreated = true;
    }
}