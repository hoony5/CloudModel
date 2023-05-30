using System.Security.Cryptography;

[Serializable]
public static class AESWrapper
{
    public static byte[] AESEncrypt<T>(T data, byte[] Key, byte[] IV) where T : class, new()
    {
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 192; // AES 192
            aes.Key = Key;
            aes.IV = IV;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(data.ToString());
                    }
                    return msEncrypt.ToArray();
                }
            }
        }
    }

    // AES Decryption
    public static T AESDecrypt<T>(byte[] cipherText, byte[] Key, byte[] IV) where T : class, new()
    {
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 192; // AES 192
            aes.Key = Key;
            aes.IV = IV;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        string decrypted = srDecrypt.ReadToEnd();
                        return (T)Convert.ChangeType(decrypted, typeof(T));
                    }
                }
            }
        }
    }

    // RSA Encryption
    public static byte[] RSAEncrypt<T>(this IdentifyInfo id, byte[] dataToEncrypt, RSAParameters RSAKeyInfo, bool doOAEPPadding) where T : class, new()
    {
        try
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(RSAKeyInfo);
                byte[] encryptedData = RSA.Encrypt(dataToEncrypt, doOAEPPadding);
                return encryptedData;
            }
        }
        catch (CryptographicException e)
        {
            AlarmModel alarm = new AlarmModel(id, new MessageInfo(e.ToString(), false));
            
            return ;
        }
    }

    // RSA Decryption
    public static byte[] RSADecrypt<T>(byte[] dataToDecrypt, RSAParameters RSAKeyInfo, bool doOAEPPadding) where T : class, new()
    {
        try
        {
            byte[] decryptedData;
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(RSAKeyInfo);
                decryptedData = RSA.Decrypt(dataToDecrypt, doOAEPPadding);
            }
            return decryptedData;
        }
        catch (CryptographicException e)
        {
            Console.WriteLine(e.ToString());
            return null;
        }
    }
}
}
