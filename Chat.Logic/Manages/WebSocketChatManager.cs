using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Chat.Data.Models.AdditionModels;
using Chat.Data.Models.DTO;
using Chat.Data.Models.Entities;
using Chat.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Chat.Logic.Manages
{
    public class WebSocketChatManager : WebSocketChatManagerBase
    {
        private readonly IChatService _chatService;

        public WebSocketChatManager(IWebSocketConnectionManager manager, IChatService chatService) : base(manager)
        {
            _chatService = chatService;
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
                        var model = DeserializeBaseMessage<MessageDTO>(baseMessage);
                        
                        UserMessageHandler(model, socket);
                    }
                    break;
                case Data.Models.AdditionModels.SocketMessageType.ServerInfo:
                        DeserializeBaseMessage<MessageEntity>(baseMessage);
                    break;
                default:

                    break;
            }


        }

        public async void UserMessageHandler(MessageDTO model, WebSocket socket)
        {
            var isSocketExist = SocketManager.GetId(socket);

            if (String.IsNullOrEmpty(isSocketExist))
            {
                await SendMessage("Bad request", socket);
            }

            var DestinationId = String.Empty;


            var isAdded = await _chatService.SaveMessageAsync(model);

            if(!isAdded)
            {
                await SendMessage("Bad request", socket);
            }

            var socketTo = SocketManager.GetSocket(model.UserIdTo);

            if (socketTo != null)
            {
                await SendMessage(model.Data, socketTo);
            }
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
