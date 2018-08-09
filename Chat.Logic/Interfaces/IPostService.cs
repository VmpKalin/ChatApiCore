using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chat.Data.Models.AdditionModels;
using Chat.Data.Models.DTO;
using Chat.Data.Models.Entities;

namespace Chat.Logic.Interfaces
{
    public interface IPostService
    {
        Task<Responce<PostEntity>> GetPostById(string id);

        Task<Responce<List<PostEntity>>> GetPostByUserId(string userId);

        Task<Responce<OperationResult>> CreatePost(PostDTO model);
    }
}
