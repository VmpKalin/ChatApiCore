using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Logic.Manages
{
    public abstract class WebSocketChatManagerBase
    {
        private WebSocketConnectionManager _socketManager;

        protected WebSocketChatManagerBase()
        {
            _socketManager = WebSocketConnectionManager.Instance();
        }

        public virtual bool AddSocket(WebSocket socket)
        {
            return _socketManager.Add(socket);
        }


        public virtual bool Remove(WebSocket socket)
        {
            return _socketManager.Remove(socket);
        }

        public abstract void Receive(HttpContext context, WebSocketReceiveResult receiveResult, byte[] buffer);

    //    var buffer = new byte[1024 * 4];
    //    WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            //while (!result.CloseStatus.HasValue)
            //{
            //    await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

            //    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            //}
        //await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
}
}
