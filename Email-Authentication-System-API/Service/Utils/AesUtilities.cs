using System.Security.Cryptography;
using System.Text;

namespace Service.Utils
{
    public class AesUtilities
    {
        private static byte[] keyArray = Encoding.UTF8.GetBytes("C#ACXJAesCode@#!");
        private static byte[] ivArray = Encoding.UTF8.GetBytes("ACXJV1024AESCODE");

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="content"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string content)
        {
            var toEncryptArray = Encoding.UTF8.GetBytes(content);

            var aes = Aes.Create();
            aes.Key = keyArray;
            aes.IV = ivArray;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            var cTransform = aes.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="content"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string content)
        {
            var toEncryptArray = Convert.FromBase64String(content);
            var aes = Aes.Create();
            aes.Key = keyArray;
            aes.IV = ivArray;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (var cTransform = aes.CreateDecryptor())
            {
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);
            }
        }
    }
}