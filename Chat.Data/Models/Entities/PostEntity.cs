using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Chat.Data.Interfaces.IEntities;
using Chat.Data.Models.DTO;
using Chat.Data.Models.Entities.LikeModels;

namespace Chat.Data.Models.Entities
{
    public class PostEntity : IEntity<string>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Time { get; set; }

        [ForeignKey("UserEntityId")]
        public string UserEntityId { get; set; }

        public LikeEntity<PostEntity> LikeInfo { get; set; } = new LikeEntity<PostEntity>();

        public PostEntity()
        {
            
        }

        public PostEntity(PostDTO model)
        {
            UserEntityId = model.UserId;
            Title = model.Title;
            Description = model.Description;
            Time = DateTime.Now;
        }
    }
}
