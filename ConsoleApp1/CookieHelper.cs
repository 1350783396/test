using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    /// <summary>
    /// 
    /// </summary>
    public class CookieHelper
    {
        /// <summary>
        /// 生意参谋data解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string sk(string str)
        {
            byte[] encryptedData = Convert.FromBase64String(HexToBase64(str));
            RijndaelManaged rijndaelCipher = new RijndaelManaged()
            {
                //Key = Encoding.UTF8.GetBytes("sycmsycmsycmsycm"),
                //IV = Encoding.UTF8.GetBytes("mcysmcysmcysmcys"),
                Key = Encoding.UTF8.GetBytes("w28Cz694s63kBYk4"),
                IV = Encoding.UTF8.GetBytes("4kYBk36s496zC82w"),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
            };
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            string result = Encoding.UTF8.GetString(plainText);
            return result;
        }
        public static string HexToBase64(string hexStr)
        {
            MatchCollection mc = Regex.Matches(hexStr.ToString(), "[A-F0-9]{2}");
            byte[] bytes = new byte[mc.Count];
            for (int i = 0; i < mc.Count; i++)
            {
                bytes[i] = byte.Parse(mc[i].Value, System.Globalization.NumberStyles.HexNumber);
            }
            return Convert.ToBase64String(bytes);
        }
    }
}
