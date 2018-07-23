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
    public class MessageEntity : IMessageEntity, IJsonConvertable, IEntity<string>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string SenderId { get; set; }

        public long Time { get; set; }

        public string Data { get; set; }

        public bool IsPrivate { get; set; }

        public string DestinationId { get; set; }


        public string ToJson() =>
            JsonConvert.SerializeObject(this);
    }
}
