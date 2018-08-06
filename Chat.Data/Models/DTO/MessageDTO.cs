using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Data.Models.DTO
{
    public class MessageDTO
    {
        public string Data { get; set; }

        public DateTime Time { get; set; }

        public string UserIdFrom { get; set; }

        public string UserIdTo { get; set; }

        public string RoomId { get; set; }
    }
}
