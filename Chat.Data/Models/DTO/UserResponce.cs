using Chat.Data.Interfaces;
using Chat.Data.Interfaces.IEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Data.Models.DTO
{
    public class UserResponce : UserCreateRequest ,IEntity<string>
    {
        public string Id { get; set; }
    }
}
