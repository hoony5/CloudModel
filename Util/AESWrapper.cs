using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using CloudModel.DataModel.Raw;

namespace CloudModel.Util;

[Serializable]
public static class AESWrapper
{
    private static (MessageInfo resultLog, byte[] encryptedData) RSAEncrypt(this byte[] key, RSAParameters RSAKeyInfo, bool doOAEPPadding)
    {
        try
        {
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(RSAKeyInfo);
            return (new MessageInfo("Success", true) ,rsa.Encrypt(key, doOAEPPadding));
        }
        catch (CryptographicException e)
        {
            return (new MessageInfo($"Failed : {e}", false), Array.Empty<byte>());
        }
    }

    private static byte[] AESEncrypt<T>(this T input, byte[] Key, byte[] IV) where T : class
    {
        byte[] encrypted;
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (var msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    // Serialize the input to a string
                    string jsonString = JsonSerializer.Serialize(input);

                    swEncrypt.Write(jsonString);
                }
                encrypted = msEncrypt.ToArray();
            }
        }
        return encrypted; 
    }
    private static (MessageInfo resultLog, string decryptedData) RSADecrypt(this byte[] dataToDecrypt, RSAParameters RSAKeyInfo, bool doOAEPPadding)
    {
        try
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(RSAKeyInfo);
                return (new MessageInfo("Success", true), Encoding.UTF8.GetString(rsa.Decrypt(dataToDecrypt, doOAEPPadding)));
            }
        }
        catch (CryptographicException e)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(RSAKeyInfo);
                return (new MessageInfo($"Failed : {e}", false),
                    Encoding.UTF8.GetString(rsa.Decrypt(dataToDecrypt, doOAEPPadding)));
            }
        }
    }

    private static T? AESDecrypt<T>(this byte[] cipherData, byte[] Key, byte[] IV) where T : class
    {
        T? result;

        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream msDecrypt = new MemoryStream(cipherData))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    // Deserialize the output from a string
                    string jsonString = srDecrypt.ReadToEnd();
                    result = JsonSerializer.Deserialize<T>(jsonString);
                }
            }
        }

        return result;
    }
    private static byte[] ComputeHMAC(this byte[] message, byte[] key)
    {
        using (HMACSHA256 hmac = new HMACSHA256(key))
        {
            return hmac.ComputeHash(message);
        }
    }

    private static bool VerifyHMAC(this byte[] message, byte[] key, byte[] sentHmac)
    {
        byte[] calcHmac = ComputeHMAC(message, key);
        return calcHmac.SequenceEqual(sentHmac);
    }

    private static (RSAParameters rsaParams, byte[] key, byte[] iv) GenerateKeys(uint length = 16)
    {
        // Generate RSA keys
        RSA rsa = RSA.Create();
        RSAParameters RSAKeyInfo = rsa.ExportParameters(true);

        // Generate AES keys
        byte[] AESKey = new byte[length];
        byte[] AESIV = new byte[length];
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(AESKey);
            rng.GetBytes(AESIV);
        }

        return (RSAKeyInfo, AESKey, AESIV);
    }
    public static (byte[] key, byte[] iv, byte[] hmac, byte[] encryptedData) EncryptMessage<T>(T data) where T : class
    {
        // Generate keys
        (RSAParameters rsaParams, byte[] aesKey, byte[] aesIv) = GenerateKeys();

        // Encrypt the data with AES
        byte[] encryptedBytesData = data.AESEncrypt(aesKey, aesIv);

        // Encrypt the AES key and IV with RSA
        (MessageInfo messageInfo, byte[] getBytes) encryptedKey = aesKey.RSAEncrypt(rsaParams, true);
        (MessageInfo messageInfo, byte[] getBytes) encryptedIV = aesIv.RSAEncrypt(rsaParams, true);
        
        // Compute HMAC of the encrypted data
        byte[] hmacBytes = encryptedBytesData.ComputeHMAC(aesKey);
        
        bool success = encryptedKey.messageInfo.Success && encryptedIV.messageInfo.Success;
        // Send the encrypted data, HMAC, encrypted AES key and IV to the client

        // return Exception Or Normal
        
        if (success) return (encryptedKey.getBytes, encryptedIV.getBytes, hmacBytes, encryptedBytesData);
        
        string log = $@"encryptedKey : {(!encryptedKey.messageInfo.Success ? encryptedKey.messageInfo : "")},
            encryptedIV : {(!encryptedIV.messageInfo.Success ? encryptedIV.messageInfo : "")}";
        
        encryptedBytesData = Encoding.UTF8.GetBytes(log);
        return (encryptedKey.getBytes, encryptedIV.getBytes, hmac qBytes, encryptedBytesData);
    }
}
