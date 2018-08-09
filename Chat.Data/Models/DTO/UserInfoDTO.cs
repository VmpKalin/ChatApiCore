using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Data.Models.DTO
{
    public class UserInfoDTO
    {
        public string Address { get; set; }

        public string Status { get; set; }

        public string AboutUser { get; set; }

        public string PhotoBase64 { get; set; }

        public DateTime Birthday { get; set; }
    }
}
