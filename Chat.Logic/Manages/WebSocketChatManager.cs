using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Chat.Data.Models.Entities;
using Chat.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Chat.Logic.Manages
{
    public class WebSocketChatManager : WebSocketChatManagerBase
    {
        public WebSocketChatManager(IWebSocketConnectionManager manager) : base(manager)
        {

        }

        public override Task<bool> OnConnected(WebSocket socket)
        {
            return base.OnConnected(socket);
        }

        public override bool OnDisconnected(WebSocket socket)
        {
            return base.OnDisconnected(socket);
        }

        public async override void Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var json = Encoding.UTF8.GetString(buffer, 0, result.Count);

            var baseMessage = JsonConvert.DeserializeObject<MessageBase>(json);
            

            switch (baseMessage.MessageType)
            {
                case Data.Models.AdditionModels.SocketMessageType.MessageType:
                    {
                        var model = DeserializeBaseMessage<MessageEntity>(baseMessage);
                        
                        UserMessageHandler(model, socket);
                    }
                    break;
                case Data.Models.AdditionModels.SocketMessageType.SingInType:
                        DeserializeBaseMessage<MessageEntity>(baseMessage);
                    break;
                case Data.Models.AdditionModels.SocketMessageType.ServerInfo:
                        DeserializeBaseMessage<MessageEntity>(baseMessage);
                    break;
                default:

                    break;
            }


        }

        public async void UserMessageHandler(MessageEntity model, WebSocket socket)
        {
            var isSocketExist = SocketManager.GetId(socket);

            var DestinationId = String.Empty;

            if(String.IsNullOrEmpty(isSocketExist))
            {
                await SendMessage("Bad request", socket);
            }

            var socketDestination = SocketManager.GetSocket(model.DestinationId);

            await SendMessage(model.Data, socketDestination);
        }

        public T DeserializeBaseMessage<T>(MessageBase model) where T : class
        {
            if(String.IsNullOrEmpty(model.Data))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(model.Data);
        }
    }
}
