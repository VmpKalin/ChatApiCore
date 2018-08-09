using Chat.Data.Models.DTO;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.Data.Models.Entities.UserModels
{
    public class UserInfoEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }

        public string AboutUser { get; set; }

        public string PhotoBase64 { get; set; }

        public DateTime Birthday { get; set; }

        [ForeignKey("UserEntityId")]
        public string UserEntityId { get; set; }

        public UserInfoEntity()
        {

        }

        public UserInfoEntity(UserInfoDTO userInfo, string userId)
        {
            Address = userInfo.Address;
            Status = userInfo.Status;
            AboutUser = userInfo.AboutUser;
            PhotoBase64 = userInfo.PhotoBase64;
            Birthday = userInfo.Birthday;
            UserEntityId = userId;
        }
    }
}
