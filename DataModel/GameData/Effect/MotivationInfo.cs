[System.Serializable]
public class MotivationInfo
{
    public string EffectName { get; private set; }
    public List<MotivationStatusInfo> MotivationStatusInfos{ get; private set; }
    
    public MotivationInfo(string effectName, List<MotivationStatusInfo> motivationStatusInfos)
    {   
        EffectName = effectName;
        MotivationStatusInfos = motivationStatusInfos;
    }
}