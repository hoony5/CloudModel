using CloudModel.DataModel.Raw;
namespace CloudModel.DataModel;

[Serializable]
public class AlarmModel
{
   public IdentifyInfo IdentifyInfo { get; private set; }
   public MessageInfo MessageInfo { get; private set; }
   
   public AlarmModel(IdentifyInfo identifyInfo, MessageInfo messageInfo)
   {
       IdentifyInfo = identifyInfo;
       MessageInfo = messageInfo;
   }
}