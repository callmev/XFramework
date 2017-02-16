using System;
using System.IO;
using System.Text;

namespace XFramework.Utilities
{
    public class FileHelper
    {
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ReadFile(string fileName)
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
                
                var context = Encoding.UTF8.GetString(data);
                return context;
            }
        }
    }
}
