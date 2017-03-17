using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DM.Common
{
    public static class CryptHelper
    {
        /// <summary>
        /// 获取字符串的Md5值.
        /// 32位MD5，小写
        /// </summary>
        /// <param name="text">明文</param>
        /// <returns>返回值为小写的32位MD5密文</returns>
        public static string Md5(string text)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            var buffer = md5.ComputeHash(Encoding.UTF8.GetBytes(text));

            var sb = new StringBuilder();
            foreach (var t in buffer)
            {
                sb.AppendFormat("{0:x2}", t);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 对称加密
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="strEncrKey"></param>
        /// <returns></returns>
        public static string Encrypt(string strText, string strEncrKey)
        {
            Byte[] byKey = new byte[strEncrKey.Length];
            Byte[] IV = { 0x7, 0xa0, 0x2f, 0x65, 0x3b, 0xb7, 0x43, 0x63 };
            try
            {
                byKey = Convert.FromBase64String(strEncrKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 对称解密
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="sDecrKey"></param>
        /// <returns></returns>
        public static string Decrypt(string strText, string sDecrKey)
        {
            Byte[] byKey = new byte[sDecrKey.Length];
            Byte[] IV = { 0x7, 0xa0, 0x2f, 0x65, 0x3b, 0xb7, 0x43, 0x63 };
            Byte[] inputByteArray = new byte[strText.Length];
            try
            {
                byKey = Convert.FromBase64String(sDecrKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
