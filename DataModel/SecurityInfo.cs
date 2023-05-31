namespace CloudModel.DataModel;

[Serializable]
public struct SecurityInfo
{
    public byte[] Hmac { get; private set; }
    public byte[] EncryptedAesKey { get; private set; }
    public byte[] EncryptedAesIv { get; private set; }
    public byte[] EncryptedData { get; private set; }
    
    
    public SecurityInfo(byte[] hmac, byte[] encryptedAesKey, byte[] encryptedAesIv, byte[] encryptedData)
    {
        Hmac = hmac;
        EncryptedAesKey = encryptedAesKey;
        EncryptedAesIv = encryptedAesIv;
        EncryptedData = encryptedData;
    }
}