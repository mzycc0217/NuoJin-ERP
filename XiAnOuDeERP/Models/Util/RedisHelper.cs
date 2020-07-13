using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace XiAnOuDeERP.Models.Util
{
    public static class RedisHelper
    {
        private static string _connIp = ConfigurationManager.AppSettings["RedisConnIp"] ?? "192.168.3.26";
        private static string _connPort = ConfigurationManager.AppSettings["RedisConnPort"] ?? "6379";
        private static string _connPassword = ConfigurationManager.AppSettings["RedisConnPassword"] ?? "775852";
        private static ConnectionMultiplexer _connMultiplexer;
        private static object _redisLocker = new object();

        public static ConnectionMultiplexer Manager
        {
            get
            {
                if (_connMultiplexer == null || !_connMultiplexer.IsConnected)
                {
                    lock (_redisLocker)
                    {
                        if (_connMultiplexer == null || !_connMultiplexer.IsConnected)
                        {
                            var option = new ConfigurationOptions()
                            {
                                Password = _connPassword,
                                EndPoints = { { _connIp, Convert.ToInt32(_connPort) } }
                            };

                            _connMultiplexer = ConnectionMultiplexer.Connect(option);
                        }
                    }
                }

                return _connMultiplexer;
            }
        }

        public static void SetConfig(string ip, string port, string password)
        {
            _connIp = ip;
            _connPort = port;
            _connPassword = password;
        }

        public static string StringGet(string key, int db = -1)
        {
            var str = Manager.GetDatabase(db).StringGet(key);

            return str;
        }

        public static bool StringSet(string key, string value, TimeSpan? cacheTime, int db = -1)
        {
            return Manager.GetDatabase(db).StringSet(key, value, cacheTime);
        }

        public static bool KeyExist(string key, int db = -1)
        {
            return Manager.GetDatabase(db).KeyExists(key);
        }

        public static List<T> ListRange<T>(string key, int index, int stop = -1, int db = -1)
        {
            var list = Manager.GetDatabase(db).ListRange(key, index);

            if (list == null)
                return null;

            var data = new List<T>();

            foreach (var item in list)
            {
                data.Add(JsonConvert.DeserializeObject<T>(item));
            }

            return data;
        }

        public static long ListRightPush<T>(string key, T value, int db = -1)
        {
            return Manager.GetDatabase(db).ListRightPush(key, JsonConvert.SerializeObject(value));
        }

        public static long ListRightPushs<T>(string key, List<T> values, int db = -1)
        {
            foreach (var item in values)
            {
                Manager.GetDatabase(db).ListRightPush(key, JsonConvert.SerializeObject(item));
            }

            return values.Count;
        }

        public static T HashGet<T>(string key, string filed, int db = -1)
        {
            var str = Manager.GetDatabase(db).HashGet(key, filed);

            if (String.IsNullOrWhiteSpace(str))
                return default(T);

            return JsonConvert.DeserializeObject<T>(str);
        }

        public static Dictionary<string, T> HashGet<T>(string key, int db = -1)
        {
            var list = Manager.GetDatabase(db).HashGetAll(key);

            if (list == null)
                return null;

            var data = new Dictionary<string, T>();

            foreach (var item in list)
            {
                data.Add(item.Name, JsonConvert.DeserializeObject<T>(item.Value));
            }

            return data;
        }


        public static bool HashSet<T>(string key, string filed, T value, TimeSpan? cacheTime, int db = -1)
        {
            return Manager.GetDatabase(db).HashSet(key, filed, JsonConvert.SerializeObject(value));
        }

        public static bool KeyDelete(string key, int db = -1)
        {
            return Manager.GetDatabase(db).KeyDelete(key);
        }

        public static bool HashDelete(string key, string field, int db = -1)
        {
            return Manager.GetDatabase(db).HashDelete(key, field);
        }

        public static bool HashDeletes<T>(string key, T[] fields, int db = -1)
        {
            foreach (var item in fields)
            {
                Manager.GetDatabase(db).HashDelete(key, JsonConvert.SerializeObject(item));
            }

            return true;
        }
    }
}