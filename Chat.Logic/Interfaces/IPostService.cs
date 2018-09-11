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
        Task<Response<PostEntity>> GetPostById(string id);

        Task<Response<List<PostEntity>>> GetPostByUserId(string userId);

        Task<Response<OperationResult>> CreatePost(PostDTO model);
    }
}
