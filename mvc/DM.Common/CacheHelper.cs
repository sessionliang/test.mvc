using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Collections;


namespace DM.Common
{
    public static class CacheHelper
    {
        public static object Read(string Key)
        {
            return HttpRuntime.Cache[Key.ToString()];
        }
        public static void Write(string Key, object CacheValue)
        {
            HttpRuntime.Cache[Key] = CacheValue;
            //if (HttpRuntime.Cache[Key] == null)
            //{
            //    HttpRuntime.Cache.Add(Key.ToString(), CacheValue, null, DateTime.Now.AddYears(10), TimeSpan.Zero,CacheItemPriority.NotRemovable, null);
            //}
            //else
            //{
            //    HttpRuntime.Cache.Insert(Key, CacheValue, null, DateTime.Now.AddYears(10), TimeSpan.Zero);
            //}
        }
        public static void Remove(string Key)
        {
            HttpRuntime.Cache.Remove(Key);
        }
        public static void Clear()
        {
            IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                string _key = CacheEnum.Key.ToString();
                HttpRuntime.Cache.Remove(_key);
            }
        }
        //public static void Write(string Key, object CacheValue, CacheDependency cd)
        //{
        //    if (HttpRuntime.Cache[Key.ToString()] == null)
        //    {
        //        HttpRuntime.Cache.Add(Key, CacheValue, cd, DateTime.Now.AddHours(10.0), TimeSpan.Zero, CacheItemPriority.NotRemovable, null);
        //    }
        //    else
        //    {
        //        HttpRuntime.Cache.Insert(Key, CacheValue, cd, DateTime.Now.AddHours(10.0), TimeSpan.Zero);
        //    }
        //}

    }
}
