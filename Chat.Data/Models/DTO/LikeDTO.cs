using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chat.Data.Models.Entities;
using Chat.Data.Models.Entities.LikeModels;

namespace Chat.Data.Models.DTO
{
    public class LikeDTO
    {
        public LikeDTO(LikeEntity<PostEntity> likeInfo)
        {
            Id = likeInfo.Id;
            CountOfLikes = likeInfo.CountOfLikes;
            UsersLike = likeInfo.LikeBindings.Select(x => x.UserId).ToList();
        }

        public string Id { get; set; }

        public int CountOfLikes { get; set; } = 0;

        public ICollection<string> UsersLike { get; set; } = new List<string>();
    }
}
