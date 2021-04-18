﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace PL.Infrastructure.Extensions
{
    public static class SessionExtension
    {
        public static void SetJson(this ISession session, string key, object value)
        {

            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
