using System.Diagnostics;
namespace CloudModel.DataModel.Raw;
[Serializable]
public class MessageInfo 
{
    public string From { get; private set; }
    public string Log { get; private set; }
    public bool Success { get; private set; }
    public long Tick { get; private set; }
    
    public MessageInfo(string from, string log, bool success)
    {
        From = from;
        Log = log;
        Success = success;
        Tick = Stopwatch.GetTimestamp();
    }
}