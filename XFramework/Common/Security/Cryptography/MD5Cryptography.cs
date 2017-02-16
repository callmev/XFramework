using System;
using System.Security.Cryptography;
using System.Text;

namespace XFramework.Common.Security.Cryptography
{
    public class Md5Cryptography
    {
        /// <summary>
        ///  MD5 加密
        /// </summary>
        /// <param name="data">需要加密的字符串</param>
        /// <returns>密文</returns>
        public static string EncryptMd5(string data)
        {
            var result = Encoding.Default.GetBytes(data);
            var md5 = new MD5CryptoServiceProvider();
            var output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }
    }
}
