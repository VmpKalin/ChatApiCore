using Chat.Logic.Manages;
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
        private readonly WebSocketChatManager _webSocketManager;
        private const int BufferSize = 4;

        public SocketMiddleware(RequestDelegate next, WebSocketChatManager webSocketManager)
        {
            _next = next;
            _webSocketManager = webSocketManager;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path != "/ws"/* && !context.WebSockets.IsWebSocketRequest*/)
            {
                await _next(context);
                return;
            }

            var socket = await context.WebSockets.AcceptWebSocketAsync();

            var isAdded = _webSocketManager.AddSocket(socket);
            if(!isAdded)
            {
                throw new Exception();
            }

            await ReceiveAsync(context, socket);
        }

        private async Task ReceiveAsync(HttpContext context, WebSocket socket)
        {
            var buffer = new byte[4 * 1024];
            var result = default(WebSocketReceiveResult);

            while (socket.State == WebSocketState.Open)
            {
                result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    if (!_webSocketManager.Remove(socket))
                        throw new Exception("Error while removing socket");
                    break;
                }
                else
                    _webSocketManager.Receive(context, result, buffer);
            }

            await socket.CloseAsync(result.CloseStatus.Value, "Socket`s closed successfully", CancellationToken.None);
        }
    }

    public static class HttpStatusCodeExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpStatusCodeExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpStatusCodeExceptionMiddleware>();
        }
    }
}
