using Chat.Logic.Manages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketServerChat.Midlewares
{
    public class SocketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly WebSocketChatManagerBase _webSocketManager;
        private const int BufferSize = 4;

        public SocketMiddleware(RequestDelegate next, WebSocketChatManagerBase webSocketManager)
        {
            _next = next;
            _webSocketManager = webSocketManager;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                await _next(context);
                return;
            }

            var socket = await context.WebSockets.AcceptWebSocketAsync();

            var isAdded = await _webSocketManager.OnConnected(socket);

            if(!isAdded)
            {
                throw new WebSocketException("Socket isn`t added");
            }

            await ReceiveAsync(context, socket);
        }

        private async Task ReceiveAsync(HttpContext context, WebSocket socket)
        {
            var buffer = new byte[BufferSize * 1024];
            var result = default(WebSocketReceiveResult);

            while (socket.State == WebSocketState.Open)
            {
                result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    if (!_webSocketManager.OnDisconnected(socket))
                        throw new WebSocketException("Error while removing socket");
                    break;
                }
                else
                    _webSocketManager.Receive(socket, result, buffer);
            }

            await socket.CloseAsync(result.CloseStatus.Value, "Socket`s closed successfully", CancellationToken.None);
        }
    }

    public static class WebSocketMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpStatusCodeExceptionMiddleware(this IApplicationBuilder builder, PathString path, WebSocketChatManagerBase handler)
        {
            return builder.Map(path,(app) => app.UseMiddleware<SocketMiddleware>(handler));
        }
    }
}
