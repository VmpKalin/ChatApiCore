using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Chat.Data.Models.AdditionModels;
using Chat.Data.Models.DTO;

namespace Chat.Data.Models.Entities.Comunity
{
    public class ComunityEntity
    {
        public ComunityEntity()
        {
            
        }

        public ComunityEntity(string userId, FollowingRequest model)
        {
            FollowingFromId = userId;
            FollowingToId = model.FollowUserId;
            Status = FollowingStatus.Pending;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [ForeignKey("UserEntityId")]
        public string FollowingFromId  { get; set; }

        [ForeignKey("UserEntityId")]
        public string FollowingToId { get; set; }

        public FollowingStatus Status { get; set; }
    }
}
