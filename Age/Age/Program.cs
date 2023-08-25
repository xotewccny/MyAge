//Nomi我知道你很好奇我的年紀，為了你我寫了一隻程式讓你玩><，無須感謝~提示我都寫在裡面了<3
//For內行人的提示，使用visual studio C# run，或是直接看exe擋，或是直接看某幾行程式碼，答案很簡單。
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

namespace Age
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Morse code:");
            Console.WriteLine("··---\n---··");
    }
        // <summary>
        // DES AES Blowfish
        //  對稱加密算法的優點是速度快，
        //  缺點是密鑰管理不方便，要求共享密鑰。
        // 可逆對稱加密，密鑰長度8
        // </summary>
        public class DesEncrypt
        {
            //private static byte[] _rgbKey = ASCIIEncoding.ASCII.GetBytes(Constant.DesKey.Substring(0, 8));
            private static byte[] _rgbKey;
            //private static byte[] _rgbIV = ASCIIEncoding.ASCII.GetBytes(Constant.DesKey.Insert(0, "w").Substring(0, 8));            //private static byte[] _rgbKey = ASCIIEncoding.ASCII.GetBytes(Constant.DesKey.Substring(0, 8));
            private static byte[] _rgbIV;

            // <summary>
            // DES 加密
            // </summary>
            // <param name="text">需要加密的值</param>
            // <returns>加密后的结果</returns>
            public static string Encrypt(string text)
            {
                DESCryptoServiceProvider dsp = new DESCryptoServiceProvider();
                using (MemoryStream memStream = new MemoryStream())
                {
                    CryptoStream crypStream = new CryptoStream(memStream, dsp.CreateEncryptor(_rgbKey, _rgbIV), CryptoStreamMode.Write);
                    StreamWriter sWriter = new StreamWriter(crypStream);
                    sWriter.Write(text);
                    sWriter.Flush();
                    crypStream.FlushFinalBlock();
                    memStream.Flush();
                    return Convert.ToBase64String(memStream.GetBuffer(), 0, (int)memStream.Length);
                }
            }

            // <summary>
            // DES解密
            // </summary>
            // <param name="encryptText"></param>
            // <returns>解密后的结果</returns>
            public static string Decrypt(string encryptText)
            {
                DESCryptoServiceProvider dsp = new DESCryptoServiceProvider();
                byte[] buffer = Convert.FromBase64String(encryptText);

                using (MemoryStream memStream = new MemoryStream())
                {
                    CryptoStream crypStream = new CryptoStream(memStream, dsp.CreateDecryptor(_rgbKey, _rgbIV), CryptoStreamMode.Write);
                    crypStream.Write(buffer, 0, buffer.Length);
                    crypStream.FlushFinalBlock();
                    return ASCIIEncoding.UTF8.GetString(memStream.ToArray());
                }
            }
        }
    }

}
