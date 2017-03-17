using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.Common
{
    public static class ConfigHelper
    {
        /// <summary>
        /// 取得默认数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultConnectionString()
        {
            string _defaultConnectionString = "";
            _defaultConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return _defaultConnectionString;
        }
        /// <summary>
        /// 获取配置字符串
        /// </summary>
        /// <param name="pKeyName"></param>
        /// <returns></returns>
        public static string GetConfigValue(string pKeyName)
        {
            string KeyValue = "";
            if (pKeyName != null)
            {
                KeyValue = System.Configuration.ConfigurationManager.AppSettings[pKeyName];
            }
            return KeyValue.Trim();
        }
    }
}
