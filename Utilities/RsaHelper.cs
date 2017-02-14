using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace XFramework.Utilities
{
    public class RsaHelper
    {
        private const string HashAlgor = "SHA1"; //MD5,SHA1,SHA256
        private const string PrivateKeyHeader = "RSA PRIVATE KEY";
        private const string PublicKeyHeader = "PUBLIC KEY";

        //public static string Encrypt(string publicKeyFileName, string originData)
        //{
        //    var rsa = LoadCertificateFile(publicKeyFileName, PublicKeyHeader);
        //    return Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(originData), false));
        //}

        //public static string Decrypt(string privateKeyFileName, string cipherData)
        //{
        //    var rsa = LoadCertificateFile(privateKeyFileName, PrivateKeyHeader);
        //    return Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(cipherData), false));
        //}

        /// <summary>
        /// 获取Hash描述表
        /// </summary>
        /// <param name="originData">待签名的字符串</param>
        /// <returns>Hash描述</returns>
        public string GetHash(string originData)
        {
            //从字符串中取得Hash描述
            var algor = HashAlgorithm.Create(HashAlgor);
            var originBytes = Encoding.UTF8.GetBytes(originData);
            var hashBytes = algor.ComputeHash(originBytes);
            var hashData = Convert.ToBase64String(hashBytes);
            return hashData;
        }
        
        #region Zcb New

        /// <summary>
        /// 加签
        /// </summary>
        /// <param name="originData"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string Signature(string originData, string privateKey)
        {
            var rsa = CreateRsaProviderFromPrivateKey(privateKey);

            //招财宝使用GBK编码
            var originBytes = Encoding.GetEncoding("GBK").GetBytes(originData);

            //蚂蚁金服使用UTF-8编码
            //var originBytes = Encoding.UTF8.GetBytes(originData);

            var signatureBytes = rsa.SignData(originBytes, HashAlgor);
            var signatureData = Convert.ToBase64String(signatureBytes);

            return signatureData;
        }

        /// <summary>
        /// 验签
        /// </summary>
        /// <param name="originData"></param>
        /// <param name="signatureData"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static bool CheckSignature(string originData, string signatureData, string publicKey)
        {
            signatureData = signatureData.Replace("\n", "");
            signatureData = signatureData.Replace("\r", "");
            publicKey = publicKey.Replace("\n", "");
            publicKey = publicKey.Replace("\r", "");

            var rsa = CreateRsaProviderFromPublicKey(publicKey);
            
            var signatureBytes = Convert.FromBase64String(signatureData);

            var originBytes = Encoding.GetEncoding("GBK").GetBytes(originData);

            //var originBytes = Encoding.UTF8.GetBytes(originData);

            var result = rsa.VerifyData(originBytes, HashAlgor, signatureBytes);

            return result;
        }

        #endregion

        private static RSACryptoServiceProvider CreateRsaProviderFromPrivateKey(string privateKey)
        {
            var privateKeyBits = Convert.FromBase64String(privateKey);

            var RSA = new RSACryptoServiceProvider();
            var RSAparams = new RSAParameters();

            using (BinaryReader binr = new BinaryReader(new MemoryStream(privateKeyBits)))
            {
                byte bt = 0;
                ushort twobytes = 0;
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    throw new Exception("Unexpected value read binr.ReadUInt16()");

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)
                    throw new Exception("Unexpected version");

                bt = binr.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binr.ReadByte()");

                RSAparams.Modulus = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Exponent = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.D = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.P = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Q = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DP = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DQ = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
            }

            RSA.ImportParameters(RSAparams);
            return RSA;
        }

        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte();
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;
            }

            while (binr.ReadByte() == 0x00)
            {
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }

        private static RSACryptoServiceProvider CreateRsaProviderFromPublicKey(string publicKey)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] x509key;
            byte[] seq = new byte[15];
            int x509size;

            x509key = Convert.FromBase64String(publicKey);
            x509size = x509key.Length;

            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            using (MemoryStream mem = new MemoryStream(x509key))
            {
                using (BinaryReader binr = new BinaryReader(mem))  //wrap Memory Stream with BinaryReader for easy reading
                {
                    byte bt = 0;
                    ushort twobytes = 0;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    seq = binr.ReadBytes(15);       //read the Sequence OID
                    if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8203)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    bt = binr.ReadByte();
                    if (bt != 0x00)     //expect null byte next
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    twobytes = binr.ReadUInt16();
                    byte lowbyte = 0x00;
                    byte highbyte = 0x00;

                    if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                        lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                    else if (twobytes == 0x8202)
                    {
                        highbyte = binr.ReadByte(); //advance 2 bytes
                        lowbyte = binr.ReadByte();
                    }
                    else
                        return null;
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                    int modsize = BitConverter.ToInt32(modint, 0);

                    int firstbyte = binr.PeekChar();
                    if (firstbyte == 0x00)
                    {   //if first byte (highest order) of modulus is zero, don't include it
                        binr.ReadByte();    //skip this null byte
                        modsize -= 1;   //reduce modulus buffer size by 1
                    }

                    byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

                    if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                        return null;
                    int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                    byte[] exponent = binr.ReadBytes(expbytes);

                    // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                    RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                    RSAParameters RSAKeyInfo = new RSAParameters();
                    RSAKeyInfo.Modulus = modulus;
                    RSAKeyInfo.Exponent = exponent;
                    RSA.ImportParameters(RSAKeyInfo);

                    return RSA;
                }
            }
        }

        private static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        #region crt 证书密钥文件读取

        public static string GetPublicKeyFromCertFile(string fileName)
        {
            var cer = X509Certificate.CreateFromCertFile(fileName);
            if (cer == null)
                throw new Exception(string.Format("{0} 不存在", fileName));

            //Console.WriteLine("hash = {0}", cer.GetCertHashString());
            //Console.WriteLine("effective Date = {0}", cer.GetEffectiveDateString());
            //Console.WriteLine("expire Date = {0}", cer.GetExpirationDateString());
            //Console.WriteLine("Issued By = {0}", cer.Issuer);
            //Console.WriteLine("Issued To = {0}", cer.Subject);
            //Console.WriteLine("algo = {0}", cer.GetKeyAlgorithm());
            //Console.WriteLine("Pub Key = {0}", cer.GetPublicKeyString());

            var cerKey = cer.GetPublicKey();
            //var publicKey = cer.GetPublicKeyString();

            var result = Convert.ToBase64String(cer.Export(X509ContentType.Cert),
                Base64FormattingOptions.InsertLineBreaks);


            return @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC7E5ecscJ8ZE6CHKBnIa5zRrbnio4Q6F1xossA00v8vVMEtANRve+bxu5seoAeDrSQBqwfCCzHCGd5eoCAoi3FUJFOKbGHIcWuV5GIwotogl3I9rfF2mrqEg7If4WZD/SHJfDNW2sxl4HDRP6x4mtB6J1RdHbBTyk9SEhLnQkHoQIDAQAB";

            #region Java方法，可以正常提取

            //FileInputStream file = new FileInputStream("server.cer");
            //CertificateFactory ft = CertificateFactory.getInstance("X.509");
            //X509Certificate certificate = (X509Certificate)ft.generateCertificate(file);
            //PublicKey publicKey = certificate.getPublicKey();
            //BASE64Encoder b64 = new BASE64Encoder();
            ////System.out.println("-----BEGIN PUBLIC KEY-----");
            //System.out.println(b64.encode(publicKey.getEncoded()));
            ////System.out.println("-----END PUBLIC KEY-----");

            #endregion

            return cer.GetPublicKeyString();
        }

        #endregion

        #region pem 密钥文件读取

        internal static string GetPublicKeyFromPemFile(string fileName)
        {
            return GetRsaKeyFromPemFile(fileName, PublicKeyHeader);
        }

        internal static string GetPrivateKeyFromPemFile(string fileName)
        {
            return GetRsaKeyFromPemFile(fileName, PrivateKeyHeader);
        }

        private static string GetRsaKeyFromPemFile(string fileName, string keyHeader)
        {
            if (!File.Exists(fileName))
            {
                throw new Exception(string.Format("文件{0}不存在。", fileName));
            }

            using (var fs = File.OpenRead(fileName))
            {
                var data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                if (data[0] == 0x30) return null;

                var pem = Encoding.UTF8.GetString(data);
                var header = String.Format("-----BEGIN {0}-----\\n", keyHeader);
                var footer = String.Format("-----END {0}-----", keyHeader);
                var start = pem.IndexOf(header, StringComparison.Ordinal) + header.Length;
                var end = pem.IndexOf(footer, start, StringComparison.Ordinal);
                var key = pem.Substring(start, (end - start));

                return key;
            }
        }

        #endregion
    }
}
