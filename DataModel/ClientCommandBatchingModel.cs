namespace CloudModel.DataModel;

[System.Serializable]
public class ClientCommandBatchingModel
{
    private Queue<ClientDataModel?> _commandQueue;
    
    public ClientCommandBatchingModel(IEnumerable<ClientDataModel> commandCollection)
    {
        _commandQueue = new Queue<ClientDataModel?>(commandCollection);
    }
    
    public bool TryDequeue(out ClientDataModel? command)
    {
        bool exist = _commandQueue.TryDequeue(out ClientDataModel? reservedCommand);
        command = exist ? reservedCommand : null;
        return exist;
    }
}