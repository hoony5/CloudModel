using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using CloudModel.DataModel;
using CloudModel.DataModel.Raw;

namespace CloudModel.Util;

public static class AESWrapper
{
    private static (MessageInfo resultLog, byte[] encryptedData) RSAEncrypt(this byte[] key, string sender, RSAParameters RSAKeyInfo, bool doOAEPPadding)
    {
        try
        {
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(RSAKeyInfo);
            return (new MessageInfo(sender, "Success", true) ,rsa.Encrypt(key, doOAEPPadding));
        }
        catch (CryptographicException e)
        {
            return (new MessageInfo(sender, $"Failed : {e}", false), Array.Empty<byte>());
        }
    }

    private static (MessageInfo messageInfo ,byte[] encryptedData) AESEncrypt<T>(this T input, string sender, byte[] Key, byte[] IV) where T : class
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
                    // Compute SHA256 hash of the data
                    byte[] hash = ComputeSHA256Hash(jsonString);

                    // Write the hash to the stream before the data
                    swEncrypt.Write(Convert.ToBase64String(hash));
                
                    // Then write the data
                    swEncrypt.Write(jsonString);
                }
                encrypted = msEncrypt.ToArray();
            }
        }
        return (new MessageInfo(sender, "Success", true),encrypted); 
    }
    private static (MessageInfo resultLog, byte[] getBytes) RSADecrypt(this byte[] dataToDecrypt,string sender, RSAParameters RSAKeyInfo, bool doOAEPPadding)
    {
        try
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(RSAKeyInfo);
                return (new MessageInfo(sender, "Success", true), rsa.Decrypt(dataToDecrypt, doOAEPPadding));
            }
        }
        catch (CryptographicException e)
        {
            return (new MessageInfo(sender, $"Failed : {e}", false), Array.Empty<byte>());
        }
    }

    private static (MessageInfo _messageInfo, T? decryptedData) AESDecrypt<T>(this byte[] cipherData,string sender, byte[] Key, byte[] IV) where T : class
    {
        T? result;
        MessageInfo messageInfo;
        
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
                    
                    // Read the hash from the stream
                    string hashString = srDecrypt.ReadLine();

                    // If the hash is null or empty, throw an exception
                    if (string.IsNullOrEmpty(hashString))
                    {
                        messageInfo = new MessageInfo(sender, "Data integrity check failed. The hash is missing.", true);
                        return (messageInfo, null);
                    }

                    // Convert the hash string back to a byte array
                    byte[] expectedHash = Convert.FromBase64String(hashString);
                
                    // Read the rest of the stream as the actual data
                    string jsonString = srDecrypt.ReadToEnd();
                
                    // Verify the hash
                    if (!jsonString.VerifySHA256Hash(expectedHash))
                    {
                        messageInfo = new MessageInfo(sender, "Data integrity check failed. The data has been modified.", false);
                        return (messageInfo, null);
                    }
                
                    result = JsonSerializer.Deserialize<T>(jsonString);
                    messageInfo = new MessageInfo(sender, "Success", true);
                }
            }
        }

        return (messageInfo, result);
    }
    private static byte[] ComputeSHA256Hash(this string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        }
    }
    private static bool VerifySHA256Hash(this string input, byte[] expectedHash)
    {
        byte[] actualHash = ComputeSHA256Hash(input);
        return actualHash.SequenceEqual(expectedHash);
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

    private static (RSAParameters rsaParams, byte[] key, byte[] iv) GenerateKeys()
    {
        // Generate RSA keys
        RSA rsaForAes = RSA.Create();

        RSAParameters rsaKeyInfoForAes = rsaForAes.ExportParameters(true);

        // Generate AES keys
        byte[] aesKey = new byte[16];
        byte[] aesIV = new byte[16];
        
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(aesKey);
            rng.GetBytes(aesIV);
        }

        return (rsaKeyInfoForAes, aesKey, aesIV);
    }
    
    // Encrypt the data with AES & RSA & HMAC
    /// <summary>
    /// Save private key safely.
    /// </summary>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static (SecurityInfo securityInfo,RSAParameters rsaParams) EncryptMessage<T>(string sender, T data) where T : class
    {
        // Generate keys
        (RSAParameters rsaParams, byte[] aesKey, byte[] aesIv) = GenerateKeys();

        // Encrypt the data with AES
        (MessageInfo messageInfo, byte[] value) encryptedData = data.AESEncrypt(sender, aesKey, aesIv);
        byte[] encryptedBytesData = encryptedData.messageInfo.Success ? encryptedData.value : Array.Empty<byte>();
        
        if(!encryptedData.messageInfo.Success)
            return (new SecurityInfo(Array.Empty<byte>(), Array.Empty<byte>(), Array.Empty<byte>(), Array.Empty<byte>()),
                rsaParams);

        // Encrypt the AES key and IV with RSA
        (MessageInfo messageInfo, byte[] getBytes) encryptedKey = aesKey.RSAEncrypt(sender, rsaParams, true);
        (MessageInfo messageInfo, byte[] getBytes) encryptedIV = aesIv.RSAEncrypt(sender, rsaParams, true);
        bool success = encryptedKey.messageInfo.Success && encryptedIV.messageInfo.Success;
        // Send the encrypted data, HMAC, encrypted AES key and IV to the client

        // Compute HMAC of the encrypted data
        byte[] hmacBytes = encryptedBytesData.ComputeHMAC(aesKey);
        if (success)
        {
            return (new SecurityInfo(hmacBytes, encryptedKey.getBytes, encryptedIV.getBytes, encryptedBytesData),
                rsaParams);
        }
        
        string log = $@"encryptedKey : {encryptedKey.messageInfo}, encryptedIV : {encryptedIV.messageInfo}";
        
        encryptedBytesData = Encoding.UTF8.GetBytes(log);
        return (new SecurityInfo(encryptedKey.getBytes, encryptedIV.getBytes, hmacBytes, encryptedBytesData), rsaParams);
    }
    
    /// <summary>
    /// Load private key safely.
    /// </summary>
    /// <param name="encryptedData"></param>
    /// <param name="hmac"></param>
    /// <param name="encryptedAesKey"></param>
    /// <param name="encryptedAesIv"></param>
    /// <param name="rsaParams"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static (MessageInfo messageInfo , T? value ) DecryptData<T>(this SecurityInfo securityInfo,string sender, RSAParameters rsaParams) where T : class
    {
        // Decrypt the AES key and IV with RSA
        (MessageInfo messageInfo, byte[] getBytes) decryptedKey = securityInfo.EncryptedAesKey.RSADecrypt(sender, rsaParams, true);
        bool isVerifyData = securityInfo.EncryptedData.VerifyHMAC(decryptedKey.getBytes, securityInfo.Hmac);
        string log = "Success";
        
        if (isVerifyData)
        {
            (MessageInfo messageInfo, byte[] getBytes) decryptedIV = securityInfo.EncryptedAesIv.RSADecrypt(sender, rsaParams, true);

            bool success = decryptedKey.messageInfo.Success && decryptedIV.messageInfo.Success;
            if (success)
            {
                (MessageInfo _messageInfo, T? decryptedData) decryptedData =
                    securityInfo.EncryptedData.AESDecrypt<T>(sender, decryptedKey.getBytes, decryptedIV.getBytes);
                
                if(decryptedData._messageInfo.Success)
                    return (decryptedData._messageInfo, decryptedData.decryptedData);
                
                return (new MessageInfo(sender, $"{log} | {decryptedData._messageInfo.From} | {decryptedData._messageInfo.Log}", true), null);
            }

            log = $@"decryptedKey : {decryptedKey.messageInfo}, decryptedIV : {decryptedIV.messageInfo}";
            return (new MessageInfo(sender, log, false), null);
        }
        
        log = $"encryptedData.VerifyHMAC : Failed";

        return (new MessageInfo(sender, log, false), null);
    }
}
