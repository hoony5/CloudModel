using CloudModel.DataModel.Raw;
namespace CloudModel.DataModel;

[System.Serializable]
public class CloudCommandBatchingModel
{
    private Queue<MessageInfo?> _messageQueue;
    
    public CloudCommandBatchingModel(IEnumerable<MessageInfo> commandCollection)
    {
        _messageQueue = new Queue<MessageInfo?>(commandCollection);
    }
    
    public bool TryDequeue(out MessageInfo? message)
    {
        bool exist = _messageQueue.TryDequeue(out MessageInfo? reservcedMessage);
        message = exist ? reservcedMessage : null;
        return exist;
    }
}
