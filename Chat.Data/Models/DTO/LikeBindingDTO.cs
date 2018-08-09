using System;
using System.Collections.Generic;
using System.Text;
using Chat.Data.Models.Entities.LikeModels;

namespace Chat.Data.Models.DTO
{
    public class LikeBindingDTO
    {
        public LikeBindingDTO(LikeBinding likeBinding)
        {
            UserId = likeBinding.UserId;
            ObjectId = likeBinding.Id;
        }

        public string UserId { get; set; }

        public string ObjectId { get; set; }
    }
}
