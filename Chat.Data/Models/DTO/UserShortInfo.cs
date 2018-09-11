using System;
using System.Collections.Generic;
using System.Text;
using Chat.Data.Models.Entities.UserModels;

namespace Chat.Data.Models.DTO
{
    public class UserShortInfo
    {
        public UserShortInfo()
        {
            
        }

        public UserShortInfo(UserEntity userEntity)
        {
            Id = userEntity.Id;
            FirstName = userEntity.FirstName;
            LastName = userEntity.LastName;
            Login = userEntity.Login;
            Status = userEntity.UserInfo.Status;
            PhotoBase64 = userEntity.UserInfo.PhotoBase64;
        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public string Status { get; set; }

        public string PhotoBase64 { get; set; }
    }
}
