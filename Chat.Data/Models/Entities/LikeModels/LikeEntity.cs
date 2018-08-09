using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Chat.Data.Interfaces.IEntities;

namespace Chat.Data.Models.Entities.LikeModels
{
    public class LikeEntity<T> : IEntity<string>
                      where T  : IEntity<string>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public int CountOfLikes { get; set; } = 0;

        public T Module { get; set; }

        [ForeignKey("ModuleId")]
        public string ModuleId { get; set; }

        public ICollection<LikeBinding> LikeBindings { get; set; } = new List<LikeBinding>();

        public LikeEntity()
        {
        }
    }
}
