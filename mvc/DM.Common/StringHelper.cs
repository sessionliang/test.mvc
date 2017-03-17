using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DM.Common
{
    public static class StringHelper
    {
        public static string EncodeToBase64(string strSource)
        {
            System.Text.UTF8Encoding oEnc = new System.Text.UTF8Encoding();
            byte[] bt = oEnc.GetBytes(strSource);
            string s1 = Convert.ToBase64String(bt);
            return s1;
        }

        public static string DecodeFromBase64(string strBase64String)
        {
            byte[] bt = Convert.FromBase64String(strBase64String);
            System.Text.UTF8Encoding oEnc = new System.Text.UTF8Encoding();
            string s1 = oEnc.GetString(bt);
            return s1;
        }
        //判断是否为汉字
        public static bool IsChinese(string ch)
        {
            byte[] byte_len = System.Text.Encoding.Default.GetBytes(ch);
            if (byte_len.Length == 2) { return true; }
            return false;
        }
        public static bool IsEnglish(string pStr)
        {
            Regex regEnglish = new Regex("^[a-zA-Z]");
            if (regEnglish.IsMatch(pStr)) { return true; }
            return false;
        }

        /// <summary>
        /// 转换数组为字符串：'a','b','c'
        /// </summary>
        /// <returns></returns>
        public static string TranslateArrayToStringWithQuote(Array list)
        {
            StringBuilder sber = new StringBuilder();
            foreach (var item in list)
            {
                if (item == null)
                {
                    continue;
                }
                if (string.IsNullOrEmpty(item.ToString()))
                {
                    continue;
                }
                sber.AppendFormat("'{0}',", item.ToString());
            }
            if (list.Length > 0 && sber.Length > 0)
            {
                sber.Length = sber.Length - 1;
            }
            return sber.ToString();
        }

        /// <summary>
        /// 转换数组为字符串：1,2,3
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string TranslateArrayToStringWithoutQuote(Array list)
        {
            StringBuilder sber = new StringBuilder();
            foreach (var item in list)
            {
                if (item == null)
                {
                    continue;
                }
                int number = 0;
                if (int.TryParse(item.ToString(), out number))
                {
                    sber.AppendFormat("{0},", number.ToString());
                }
            }
            if (list.Length > 0 && sber.Length > 0)
            {
                sber.Length = sber.Length - 1;
            }
            return sber.ToString();
        }
    }
}
