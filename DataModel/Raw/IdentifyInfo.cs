[Serializable]
public class IdentifyInfo
{
    public string ProjectID {get; private set;}
    public string PlayerID {get; private set;}
    public string AccessToken {get; private set;}
    public IdentifyInfo(string projectId, string playerId, string accessToken)
    {
        ProjectID = projectId;
        PlayerID = playerId;
        AccessToken = accessToken;
    }
}