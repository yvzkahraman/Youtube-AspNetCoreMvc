using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Extensions
{
    public static class ExtensionSession
    {
        public static void SetObject(this ISession session, string key, object deger)
        {
            session.SetString(key, JsonConvert.SerializeObject(deger));
        }

        public static T GetObject<T>(this ISession session, string key) where T : class
        {
            var gelenData = session.GetString(key);
            if (gelenData != null)
            {
                return JsonConvert.DeserializeObject<T>(gelenData);
            }
            return null;
          
        }
    }
}
