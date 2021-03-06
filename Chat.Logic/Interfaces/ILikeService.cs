﻿using Chat.Data.Models.AdditionModels;
using Chat.Data.Models.Entities;
using Chat.Data.Models.Entities.LikeModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chat.Data.Models.DTO;

namespace Chat.Logic.Interfaces
{
    public interface ILikeService
    {
        Task<Response<LikeAction>> CreateLike(string postId, string userLikeFromId);

        Task<Response<LikeDTO>> GetLikesByPostId(string postId);
    }
}
