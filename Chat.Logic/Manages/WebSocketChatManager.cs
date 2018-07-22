using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Chat.Logic.Manages
{
    public class WebSocketChatManager : WebSocketChatManagerBase
    {
        public override void Receive(HttpContext context, WebSocketReceiveResult receiveResult, byte[] buffer)
        {
            throw new NotImplementedException();
        }
    }
}
