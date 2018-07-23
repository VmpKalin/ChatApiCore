using Chat.Data.Extensions;
using Chat.Data.Interfaces.Helpers;
using Chat.Data.Models.AdditionModels;
using Chat.Data.Models.Entities;
using Chat.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Logic.Manages
{
    public abstract class WebSocketChatManagerBase
    {
        private IWebSocketConnectionManager _socketManagerUsers;
        private IWebSocketConnectionManager _socketManagerGroups;

        public IWebSocketConnectionManager SocketManager => _socketManagerUsers;

        public IWebSocketConnectionManager SocketManagerGroups => _socketManagerGroups;

        protected WebSocketChatManagerBase(IWebSocketConnectionManager manager)
        {
            _socketManagerUsers = manager;
        }

        public virtual async Task<bool> OnConnected(WebSocket socket)
        {
            var result = SocketManager.Add(socket);

            if(!result)
                return result;

            var message = new MessageBase()
            {
                MessageType = SocketMessageType.ServerInfo,
                Data = "Hello, ur successfully connected to server!"
            };

            var data = GetReadyMessage(message, socket);

            await SendMessage(data.Item1, data.Item2);

            return result; 
        }

        public virtual bool OnDisconnected(WebSocket socket) =>
            SocketManager.Remove(socket);
        

        public virtual async Task SendMessage<T>(T data, WebSocket socket) where T : IJsonConvertable
        {
            var message = GetReadyMessage(data, socket);

            var m = GetReadyMessage(data, socket);

            await SendMessage(message.Item1, message.Item2);
        }

        public virtual async Task SendMessage(string data, WebSocket socket)
        {
            var byteArray = Encoding.ASCII.GetBytes(data.ToJson());

            var arraySegment = new ArraySegment<byte>(byteArray, 0, byteArray.Length);

            await SendMessage(arraySegment, socket);
        }

        public virtual async Task SendMessage(ArraySegment<byte> byteArray, WebSocket socket)
        {
            await socket.SendAsync(byteArray, WebSocketMessageType.Text, true, CancellationToken.None);
        }
        
        public virtual async Task SendMessageToAll<T>(T data) where T : IJsonConvertable
        {
            foreach (var socket in SocketManager.Sockets.Values)
            {
                await SendMessage(data, socket);
            }
        }


        public virtual Tuple<ArraySegment<byte>, WebSocket> GetReadyMessage<T>(T data, WebSocket socket) where T : IJsonConvertable
        {
            var byteArray = Encoding.ASCII.GetBytes(data.ToJson());

            var arraySegment = new ArraySegment<byte>(byteArray, 0, byteArray.Length);
            
            return Tuple.Create(arraySegment, socket);
        }

        public abstract void Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}
