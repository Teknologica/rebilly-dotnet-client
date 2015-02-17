using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Web;

namespace Rebilly.helper
{
    public class HttpBuildQuery
    {
        public static string build(Dictionary<string, string> queryParam)
        {
            List<string> param = new List<string>();
            foreach (var entry in queryParam)
            {
                param.Add(entry.Key + "=" + entry.Value);
            }
            return String.Join("&", param.ToArray());
        }
    }
}
