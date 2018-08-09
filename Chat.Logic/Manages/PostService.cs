using Chat.Data.Context;
using Chat.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Data.Models.AdditionModels;
using Chat.Data.Models.DTO;
using Chat.Data.Models.Entities;
using Chat.Data.Models.Entities.LikeModels;
using Microsoft.EntityFrameworkCore;

namespace Chat.Logic.Manages
{
    public class PostService : IPostService
    {
        private readonly GeneralContext _context;

        public PostService(GeneralContext context)
        {
            _context = context;
        }

        public async Task<Responce<List<PostEntity>>> GetPostByUserId(string userId)
        {
            var responce = new Responce<List<PostEntity>>();

            var posts = await _context.Posts.Where(x => x.UserEntityId == userId).ToListAsync();

            if (posts == null)
            {
                responce.Error = new Error("Posts can`t be founded by user id");
                return responce;
            }

            responce.Data = posts;
            return responce;
        }

        // GET: api/Post/5
        public async Task<Responce<PostEntity>> GetPostById(string id)
        {
            var responce = new Responce<PostEntity>();

            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);

            if (post == null)
            {
                responce.Error = new Error("Post can`t be founded by id");
                return responce;
            }

            responce.Data = post;
            
            return responce;
        }

        public async Task<Responce<OperationResult>> CreatePost(PostDTO model)
        {
            var responce = new Responce<OperationResult>();

            var entity = new PostEntity(model);

            await _context.Posts.AddAsync(entity);
            await _context.SaveChangesAsync();

            var likeInfo = new LikeEntity<PostEntity> { ModuleId = entity.Id };
            await _context.Likes.AddAsync(likeInfo);
            await _context.SaveChangesAsync();

            responce.Data = OperationResult.Success;
            return responce;
        }
    }
}
