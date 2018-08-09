using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chat.Data.Models.DTO
{
    public class PostDTO
    {
        public string UserId { get; set; }

        [Required]
        //[Range(3,50,ErrorMessage = "Title can be from 3 to 50 chars")]
        public string Title { get; set; }

        [Required]
        //[Range(20, 500, ErrorMessage = "Title can be from 20 to 500 chars")]
        public string Description { get; set; }
    }
}
