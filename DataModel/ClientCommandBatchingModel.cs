using CloudModel.DataModel;

[System.Serializable]
public class ClientCommandBatchingModel
{
    private Queue<ClientDataModel> commandQueue;
    
    public ClientCommandBatchingModel(IEnumerable<ClientDataModel> commandCollection)
    {
        commandQueue = new Queue<ClientDataModel?>(commandCollection);
    }
    
    public bool TryDequeue(out ClientDataModel command)
    {
        bool exist = commandQueue.TryDequeue(out ClientDataModel reservedCommand);
        command = exist ? reservedCommand : null;
        return exist;
    }
}
