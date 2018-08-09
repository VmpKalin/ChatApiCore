using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Data.Models.DTO
{
    public class UserCreationRequest : UserDTO
    {
        public string Password { get; set; }
    }
}
