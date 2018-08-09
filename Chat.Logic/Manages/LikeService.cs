using Chat.Data.Context;
using Chat.Data.Models.AdditionModels;
using Chat.Data.Models.Entities;
using Chat.Data.Models.Entities.LikeModels;
using Chat.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Data.Models.DTO;
using Microsoft.EntityFrameworkCore.Query;

namespace Chat.Logic.Manages
{
    public class LikeService : ILikeService
    {
        private readonly GeneralContext _context;

        public LikeService(GeneralContext context)
        {
            _context = context;
        }

        public async Task<Responce<LikeDTO>> GetLikesByPostId(string postId)
        {
            var responce = new Responce<LikeDTO>();

            var post = await _context.Posts.Include(x => x.LikeInfo)
                                           .ThenInclude(x => x.LikeBindings)
                                           .FirstOrDefaultAsync(x => x.Id == postId);

            if(post == null)
            {
                responce.Error = new Error("post isn`t founded by id");
                return responce;
            }

            responce.Data = new LikeDTO(post.LikeInfo);

            return responce;
        }

        public async Task<Responce<LikeAction>> CreateLike(string postId, string userLikeFromId)
        {
            var responce = new Responce<LikeAction>();

            var likeInfo = await _context.Likes.Include(x => x.LikeBindings).FirstOrDefaultAsync(x => x.ModuleId == postId);

            if(likeInfo==null)
            {
                responce.Error = new Error("Can`t find post by id");
                return responce;
            }

            var isLikeExistFromUser = likeInfo.LikeBindings.Any(x => x.UserId == userLikeFromId);
            
            if (isLikeExistFromUser)
            {
                var like = likeInfo.LikeBindings.FirstOrDefault(x => x.UserId == userLikeFromId);

                _context.LikeBindings.Remove(like);
                await _context.SaveChangesAsync();

                likeInfo.LikeBindings.Remove(like);
                likeInfo.CountOfLikes--;
                _context.Likes.Update(likeInfo);
                await _context.SaveChangesAsync();

                responce.Data = LikeAction.LikeRemoved;
                return responce;
            }
            else
            {

                var like = new LikeBinding(userLikeFromId, likeInfo.Id);
                await _context.LikeBindings.AddAsync(like);
                await _context.SaveChangesAsync();

                likeInfo.LikeBindings.Add(like);
                likeInfo.CountOfLikes++;
                _context.Likes.Update(likeInfo);
                await _context.SaveChangesAsync();


                responce.Data = LikeAction.LikeAdded;
                return responce;
            }
        }
    }
}
