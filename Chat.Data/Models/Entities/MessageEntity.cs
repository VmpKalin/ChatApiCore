using Chat.Data.Interfaces.Helpers;
using Chat.Data.Interfaces.IEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Chat.Data.Models.Entities
{
    public class MessageEntity : IMessageEntity, IEntity<string>, IJsonConvertable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public DateTime Time { get; set; }

        public string Data { get; set; }

        public RoomEntity RoomEntity { get; set; }

        public MessageEntity(string data, DateTime time, RoomEntity roomEntity)
        {
            Id = Guid.NewGuid().ToString();
            Time = time;
            Data = data;
            RoomEntity = roomEntity;
        }

        public string ToJson() =>
            JsonConvert.SerializeObject(this);
    }
}
