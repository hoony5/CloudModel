namespace CloudModel.DataModel.Raw;
[Serializable]
public class IdentifyInfo
{
    public string PlayerID {get; private set;}
    public string AccessToken {get; private set;}
    public IdentifyInfo(string playerId, string accessToken)
    {
        PlayerID = playerId;
        AccessToken = accessToken;
    }
    public override string ToString()
    {
        return $@"PlayerID :: {PlayerID} |
AccessToken :: {AccessToken}" + '\n';
    }
}