using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace JiaMi
{
    public static class Sm3Crypto
    {
        /// <summary>
        /// sm3加密(使用自定义密钥)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToSM3byte(string data, string key)
        {
            byte[] msg1 = Encoding.Default.GetBytes(data);
            byte[] key1 = Encoding.Default.GetBytes(key);

            KeyParameter keyParameter = new KeyParameter(key1);
            SM3Digest sm3 = new SM3Digest();

            HMac mac = new HMac(sm3);//带密钥的杂凑算法
            mac.Init(keyParameter);
            mac.BlockUpdate(msg1, 0, msg1.Length);
            byte[] result = new byte[mac.GetMacSize()];

            mac.DoFinal(result, 0);
            return Hex.Encode(result);
        }

        /// <summary>
        /// sm3加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns>二进制数组</returns>
        public static byte[] ToSM3byte(string data)
        {
            var msg = Encoding.Default.GetBytes(data);//把字符串转成16进制的ASCII码 
            SM3Digest sm3 = new SM3Digest();
            sm3.BlockUpdate(msg, 0, msg.Length);
            byte[] md = new byte[sm3.GetDigestSize()];//SM3算法产生的哈希值大小
            sm3.DoFinal(md, 0);
            return Hex.Encode(md);
        }

        /// <summary>
        /// sm3加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns>16进制字符串</returns>
        public static string ToSM3HexStr(string data)
        {
            var msg = Encoding.Default.GetBytes(data);//把字符串转成16进制的ASCII码 
            SM3Digest sm3 = new SM3Digest();
            sm3.BlockUpdate(msg, 0, msg.Length);
            byte[] md = new byte[sm3.GetDigestSize()];//SM3算法产生的哈希值大小
            sm3.DoFinal(md, 0);
            return new UTF8Encoding().GetString(Hex.Encode(md));
        }

        /// <summary>
        /// sm3加密(使用自定义Hex密钥)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ToSM3HexStr(string data, string key)
        {
            byte[] msg1 = Encoding.Default.GetBytes(data);
            byte[] key1 = System.Text.Encoding.UTF8.GetBytes(key); 

            KeyParameter keyParameter = new KeyParameter(key1);
            SM3Digest sm3 = new SM3Digest();

            HMac mac = new HMac(sm3);//带密钥的杂凑算法
            mac.Init(keyParameter);
            mac.BlockUpdate(msg1, 0, msg1.Length);
            byte[] result = new byte[mac.GetMacSize()];

            mac.DoFinal(result, 0);
            return new UTF8Encoding().GetString(Hex.Encode(result));
        }

        /// <summary>
        /// 16进制格式字符串转字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] HexStringToBytes(string hexString)
        {
            hexString = Regex.Replace(hexString, @".{2}", "$0 ");
            //以 ' ' 分割字符串，并去掉空字符
            string[] chars = hexString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] returnBytes = new byte[chars.Length];
            //逐个字符变为16进制字节数据
            for (int i = 0; i < chars.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(chars[i], 16);
            }
            return returnBytes;
        }
    }
}
