using CloudModel.DataModel.Raw;

[System.Serializable]
public class CloudCommandBatchingModel
{
    private Queue<MessageInfo> messageQueue;
    
    public CloudCommandBatchingModel(IEnumerable<MessageInfo> commandCollection)
    {
        messageQueue = new Queue<MessageInfo?>(commandCollection);
    }
    
    public bool TryDequeue(out MessageInfo message)
    {
        bool exist = messageQueue.TryDequeue(out MessageInfo reservcedMessage);
        message = exist ? reservcedMessage : null;
        return exist;
    }
}
