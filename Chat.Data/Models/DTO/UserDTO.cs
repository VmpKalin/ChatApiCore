using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Chat.Data.Models.Entities;
using Chat.Data.Models.Entities.UserModels;

namespace Chat.Data.Models.DTO
{
    public class UserDTO
    {
        [MaxLength(15, ErrorMessage = "FirstName can`t be bigger than 15 chars")]
        public string FName { get; set; }

        [MaxLength(15, ErrorMessage = "FirstName can`t be bigger than 15 chars")]
        public string LName { get; set; }

        [Required]
        //[EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Login { get; set; }

        public UserDTO(UserEntity model)
        {
            FName = model.FirstName;
            LName = model.LastName;
            Email = model.Email;
            Login = model.Login;
        }

        public UserDTO()
        {
            
        }
    }
}
