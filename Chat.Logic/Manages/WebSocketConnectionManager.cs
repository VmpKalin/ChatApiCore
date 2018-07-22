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
    public class WebSocketConnectionManager
    {
        private readonly ConcurrentDictionary<Guid, WebSocket> _sockets;
        private static WebSocketConnectionManager _instance;

        private WebSocketConnectionManager()
        { }

        public ConcurrentDictionary<Guid, WebSocket> Sockets => _sockets;

        public static WebSocketConnectionManager Instance()
        {
            if (_instance == null)
                _instance = new WebSocketConnectionManager();
            return _instance;
        }
        
        public bool Add(WebSocket socket) =>
            _sockets.TryAdd(Guid.NewGuid(),socket);
        
        public bool Remove(WebSocket socket) =>
            _sockets.TryRemove(
                _sockets.FirstOrDefault(x=> x.Value == socket).Key, 
                out socket );
    }
}
