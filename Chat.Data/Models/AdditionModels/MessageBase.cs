using Chat.Data.Interfaces.Helpers;
using Chat.Data.Models.AdditionModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Data.Models.AdditionModels
{
    public class MessageBase : IJsonConvertable
    {
        public SocketMessageType MessageType { get; set; }

        public string Data { get; set; }

        public string ToJson() =>
            JsonConvert.SerializeObject(this);
    }
}
