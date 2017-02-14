using System;
using System.Security.Cryptography;
using System.Text;

namespace XFramework.Common.Security.Cryptography
{
    public class DesCryptography
    {
        /// <summary>
        /// 3DES 加密算法
        /// </summary>
        /// <param name="data">需要加密的字符串</param>
        /// <param name="key">加密的 key</param>
        /// <returns>密文</returns>
        public static string Encrypt3Des(string data, string key)
        {
            var des = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.Default.GetBytes(Md5Cryptography.EncryptMd5(key).Substring(0, 24)),
                Mode = CipherMode.ECB
            };

            var desEncrypt = des.CreateEncryptor();
            var buffer = Encoding.Default.GetBytes(data);

            return Convert.ToBase64String(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
        }

        /// <summary>
        /// 3DES 解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="key">解密 key</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt3Des(string data, string key)
        {
            var des = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.Default.GetBytes(Md5Cryptography.EncryptMd5(key).Substring(0, 24)),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var desDecrypt = des.CreateDecryptor();
            var buffer = Convert.FromBase64String(data);

            return Encoding.Default.GetString(desDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));

        }
    }
}
