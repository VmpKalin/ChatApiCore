using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace Chat.Logic.Interfaces
{
    public interface IWebSocketConnectionManager
    {
        ConcurrentDictionary<string, WebSocket> Sockets { get; }

        bool Add(WebSocket socket);
        string CreateConnectionId();
        string GetId(WebSocket socket);
        WebSocket GetSocket(string id);
        bool Remove(WebSocket socket);
    }
}