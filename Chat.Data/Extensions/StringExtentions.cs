using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Data.Extensions
{
    public static class StringExtentions
    {
        public static string ToJson(this string str)
        {
            return JsonConvert.SerializeObject(str);
        }
    }
}
