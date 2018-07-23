using Chat.Logic.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Logic.Manages
{
    public class WebSocketConnectionManager : IWebSocketConnectionManager
    {
        private readonly ConcurrentDictionary<string, WebSocket> _sockets;

        public WebSocketConnectionManager()
        {
            _sockets = new ConcurrentDictionary<string, WebSocket>();
        }

        public ConcurrentDictionary<string, WebSocket> Sockets => _sockets;
        
        public bool Add(WebSocket socket) =>
            _sockets.TryAdd(CreateConnectionId(),socket);
        
        public bool Remove(WebSocket socket) =>
            _sockets.TryRemove(
                _sockets.FirstOrDefault(x=> x.Value == socket).Key, 
                out socket );

        public string GetId(WebSocket socket) =>
            _sockets.FirstOrDefault(x => x.Value == socket).Key;

        public WebSocket GetSocket(string id) =>
            _sockets.FirstOrDefault(x => x.Key == id).Value;

        public string CreateConnectionId() =>
            Guid.NewGuid().ToString();
    }
}
