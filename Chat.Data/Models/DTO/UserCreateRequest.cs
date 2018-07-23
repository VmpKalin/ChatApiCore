using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chat.Data.Models.DTO
{
    public class UserCreateRequest
    {
        [Required]
        [MaxLength(15, ErrorMessage = "FirstName can`t be bigger than 15 chars")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "FirstName can`t be bigger than 15 chars")]
        public string FirstName { get; set; }

        [Range(18, 99, ErrorMessage = "Year range for registration from 18 to 99")]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
