using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestDll
{
    [Guid("70C87B2D-4151-4999-928B-167509E0349B")]
    public interface CallParamers 
    {
        [DispId(1)]
        string ZJ_Hmac_SM3(string key, string secret, string unix_timestamp, string request_body);
    }
    [Guid("A7B32690-6A3D-460D-BF54-29C5D4A9EA7E"),ClassInterface(ClassInterfaceType.None),ComDefaultInterface(typeof(CallParamers))]
    public class Class1 : CallParamers
    {
        public string ZJ_Hmac_SM3(string key, string secret, string unix_timestamp, string request_body)
        {
            var stringToSign = $"{unix_timestamp}\n{ request_body}";
            byte[] msg1 = Encoding.UTF8.GetBytes(stringToSign);
            byte[] key1 = Encoding.UTF8.GetBytes(secret);

            Org.BouncyCastle.Crypto.Parameters.KeyParameter keyParameter = new Org.BouncyCastle.Crypto.Parameters.KeyParameter(key1);
            Org.BouncyCastle.Crypto.Digests.SM3Digest sm3 = new Org.BouncyCastle.Crypto.Digests.SM3Digest();

            Org.BouncyCastle.Crypto.Macs.HMac mac = new Org.BouncyCastle.Crypto.Macs.HMac(sm3);//带密钥的杂凑算法
            mac.Init(keyParameter);
            mac.BlockUpdate(msg1, 0, msg1.Length);
            byte[] result = new byte[mac.GetMacSize()];

            mac.DoFinal(result, 0);
            return new UTF8Encoding().GetString(Org.BouncyCastle.Utilities.Encoders.Hex.Encode(result)).ToUpper();
        }
    }

}
