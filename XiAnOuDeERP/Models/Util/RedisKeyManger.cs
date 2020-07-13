using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiAnOuDeERP.Models.Util
{
    /// <summary>
    /// Redis 键值管理
    /// </summary>
    public static class RedisKeyManger
    {
        public static string GetSystemKey()
        {
            return "system";
        }

        public static string GetUserProgressKey(long userId, string code)
        {
            return string.Format("user:{0}:{1}", userId, code);
        }

        public static string GetOpenTokenKey(string key)
        {
            return String.Format("opentoken:{0}", key);
        }

        public static string GetAppTokenKey(long key)
        {
            return String.Format("apptoken:{0}", key);
        }

        public static string GetWebTokenKey(long key)
        {
            return String.Format("webtoken:{0}", key);
        }

        public static string GetUserKey(string key)
        {
            return String.Format("user:{0}", key);
        }

        public static string GetUserRoleKey(string key)
        {
            return String.Format("userrole:{0}", key);
        }

    }
}