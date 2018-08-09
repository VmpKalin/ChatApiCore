using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Chat.Data.Interfaces.IEntities;

namespace Chat.Data.Models.Entities.LikeModels
{
    public class LikeBinding : IEntity<string>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("LikeEntityId")]
        public string LikeEntityId { get; set; }

        public DateTime Time { get; set; }

        public LikeBinding(string userId, string likeId)
        {
            UserId = userId;
            LikeEntityId = likeId;
            Time = DateTime.Now;
        }

        public LikeBinding()
        {

        }

    }
}
