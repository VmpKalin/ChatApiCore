using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Data.Interfaces.Helpers
{
    public interface IJsonConvertable
    {
        string Data { get; }
        string ToJson();
    }
}
