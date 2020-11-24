using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    public class CamCrypto
    {
        private static readonly string key = "fe9b21b6f32b4d538d69fb465a7a034c";
        private const string ENCODE = "UTF-8";
        /// <summary>
        /// DES字符串加密
        /// </summary>
        /// <param name="_strQ"></param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt)
        {
            //return toEncrypt;
            string strPassword = generateKey(key);
            byte[] keyArray = System.Text.Encoding.GetEncoding(ENCODE).GetBytes(strPassword);
            byte[] toEncryptArray = System.Text.Encoding.GetEncoding(ENCODE).GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        ///  DES字符串解密
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt)
        {
            // return toDecrypt;

            try
            {
                if (null == toDecrypt)
                {
                    return toDecrypt;
                }
                string strPassword = generateKey(key);
                byte[] keyArray = System.Text.Encoding.GetEncoding(ENCODE).GetBytes(strPassword);
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return System.Text.Encoding.GetEncoding(ENCODE).GetString(resultArray);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private static string generateKey(string str)
        {
            if (null == str)
            {
                str = "defaultpassword";
            }
            else if (str.Length < 1)
            {
                str = "emptypassword";
            }
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.GetEncoding(ENCODE).GetBytes(str);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();

            String strret = Convert.ToBase64String(bytes);
            while (strret.Length < 16)
            {
                strret += "%";
            }
            if (strret.Length > 16)
            {
                int nbegin = (strret.Length - 16) / 2;
                strret = strret.Substring(nbegin, 16);
            }
            return strret;
        }

        /// <summary>
        /// MD5
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public static string MD5(string express)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] palindata = Encoding.Default.GetBytes(express);//将要加密的字符串转换为字节数组
            byte[] encryptdata = md5.ComputeHash(palindata);//将字符串加密后也转换为字符数组
            return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为加密字符串
        }
    }
}
