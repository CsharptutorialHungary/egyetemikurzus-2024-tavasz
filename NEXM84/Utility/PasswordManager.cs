using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NEXM84.Utility
{
    internal sealed class PasswordManager : Infrastructure.IPasswordManager
    {
        private static readonly byte[] Key = new byte[]
        {
        0x4E, 0xA1, 0x6F, 0x7B, 0x6A, 0x54, 0x2E, 0x0F, 0x43, 0x97, 0x26, 0x3B, 0xC5, 0x92, 0xAE, 0xD8
        };

        private static readonly byte[] IV = new byte[]
        {
        0x6D, 0x5F, 0x92, 0xE9, 0xC4, 0x12, 0x75, 0xF0, 0xA3, 0x8B, 0x07, 0x4E, 0x9F, 0x63, 0xD7, 0x1A
        };

        public string AESEncryptPassword(string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(password);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public string DecryptPassword(string encryptedPassword)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedPassword)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}
