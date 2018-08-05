using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chat.Data.Models.DTO
{
    public class UserCreateRequest
    {
        [MaxLength(15, ErrorMessage = "FirstName can`t be bigger than 15 chars")]
        public string FName { get; set; }

        [MaxLength(15, ErrorMessage = "FirstName can`t be bigger than 15 chars")]
        public string LName { get; set; }

        [Required]
        //[EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        [Range(18, 99, ErrorMessage = "Age must be from 18 to 99 years")]
        public int Age { get; set; }
    }
}
