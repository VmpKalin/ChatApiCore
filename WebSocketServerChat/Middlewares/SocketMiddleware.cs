using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSocketServerChat.Midlewares
{
    public class SocketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly WebSocketManager _webSocketManager;

        public SocketMiddleware(RequestDelegate next, WebSocketManager webSocketManager,)
        {
            _next = next;
            _webSocketManager = webSocketManager;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/ws")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var socket = await _webSocketManager.AcceptWebSocketAsync();
                    await 
                }
            }






        }
    }
}
