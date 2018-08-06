using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Data.Models.Entities
{
    public class RoomEntity
    {
        public string Id { get; set; }

        public UserEntity UserFrom { get; set; }

        public UserEntity UserTo { get; set; }

        public ICollection<MessageEntity> Messages { get; set; } = new List<MessageEntity>();

        public string LastMessage { get; set; }

        public int UnreadedMessages { get; set; }

        public RoomEntity(string id, UserEntity userFrom, UserEntity userTo, ICollection<MessageEntity> messages, string lastMessage, int unreadedMessages)
        {
            Id = id;
            UserFrom = userFrom;
            UserTo = userTo;
            Messages = messages;
            LastMessage = lastMessage;
            UnreadedMessages = unreadedMessages;
        }

        public RoomEntity()
        {

        }

        public RoomEntity(UserEntity from, UserEntity to, string message)
        {
            Id = Guid.NewGuid().ToString();
            UserFrom = from;
            UserTo = to;
            LastMessage = message;
        }
    }
}
