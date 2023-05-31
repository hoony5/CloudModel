using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using CloudModel.DataModel;
using CloudModel.DataModel.Raw;

namespace CloudModel.Util;

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
    private static (MessageInfo resultLog, byte[] getBytes) RSADecrypt(this byte[] dataToDecrypt, RSAParameters RSAKeyInfo, bool doOAEPPadding)
    {
        try
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(RSAKeyInfo);
                return (new MessageInfo("Success", true), rsa.Decrypt(dataToDecrypt, doOAEPPadding));
            }
        }
        catch (CryptographicException e)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(RSAKeyInfo);
                return (new MessageInfo($"Failed : {e}", false), rsa.Decrypt(dataToDecrypt, doOAEPPadding));
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

    private static (RSAParameters rsaParams, byte[] key, byte[] iv) GenerateKeys()
    {
        // Generate RSA keys
        RSA rsa = RSA.Create();
        // no contains private key
        RSAParameters rsaKeyInfo = rsa.ExportParameters(false);

        // Generate AES keys
        byte[] aesKey = new byte[16];
        byte[] aesIV = new byte[16];
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(aesKey);
            rng.GetBytes(aesIV);
        }

        return (rsaKeyInfo, aesKey, aesIV);
    }
    
    // Encrypt the data with AES & RSA & HMAC
    /// <summary>
    /// Save private key safely.
    /// </summary>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static (SecurityInfo securityInfo,RSAParameters rsaParams) EncryptMessage<T>(T data) where T : class
    {
        // Generate keys
        (RSAParameters rsaParams, byte[] aesKey, byte[] aesIv) = GenerateKeys();

        // Encrypt the data with AES
        byte[] encryptedBytesData = data.AESEncrypt(aesKey, aesIv);

        // Encrypt the AES key and IV with RSA
        (MessageInfo messageInfo, byte[] getBytes) encryptedKey = aesKey.RSAEncrypt(rsaParams, true);
        (MessageInfo messageInfo, byte[] getBytes) encryptedIV = aesIv.RSAEncrypt(rsaParams, true);
        bool success = encryptedKey.messageInfo.Success && encryptedIV.messageInfo.Success;
        // Send the encrypted data, HMAC, encrypted AES key and IV to the client

        // Compute HMAC of the encrypted data
        byte[] hmacBytes = encryptedBytesData.ComputeHMAC(aesKey);
        // return Exception Or Normal
        
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
    public static (MessageInfo messageInfo , T? value ) DecryptData<T>(this SecurityInfo securityInfo, RSAParameters rsaParams) where T : class
    {
        // Decrypt the AES key and IV with RSA
        (MessageInfo messageInfo, byte[] getBytes) decryptedKey = securityInfo.EncryptedAesKey.RSADecrypt(rsaParams, true);
        bool isVerifyData = securityInfo.EncryptedData.VerifyHMAC(decryptedKey.getBytes, securityInfo.Hmac);
        string log = "Success";
        
        if (isVerifyData)
        {
            (MessageInfo messageInfo, byte[] getBytes) decryptedIV = securityInfo.EncryptedAesIv.RSADecrypt(rsaParams, true);

            bool success = decryptedKey.messageInfo.Success && decryptedIV.messageInfo.Success;
            if (success)
            {
                return (new MessageInfo(log, true), securityInfo.EncryptedData.AESDecrypt<T>(decryptedKey.getBytes, decryptedIV.getBytes));
            }

            log = $@"decryptedKey : {decryptedKey.messageInfo}, decryptedIV : {decryptedIV.messageInfo}";
            return (new MessageInfo(log, false), null);
        }
        
        log = $"encryptedData.VerifyHMAC : Failed";

        return (new MessageInfo(log, false), null);
    }
}
