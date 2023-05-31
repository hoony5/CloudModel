using System.Diagnostics;
namespace CloudModel.DataModel.Raw;
[Serializable]
public class MessageInfo 
{
    public string Log { get; private set; }
    public bool Success { get; private set; }
    public long Tick { get; private set; }
    
    public MessageInfo(string log, bool success)
    {
        Log = log;
        Success = success;
        Tick = Stopwatch.GetTimestamp();
    }
}