using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Data.Context;
using Chat.Data.Models.AdditionModels;
using Chat.Data.Models.DTO;
using Chat.Data.Models.Entities;
using Chat.Data.Models.Entities.Comunity;
using Chat.Data.Models.Entities.LikeModels;
using Chat.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.Logic.Manages
{
    public class FriendService : IFriendService
    {
        private readonly GeneralContext _context;

        public FriendService(GeneralContext context)
        {
            _context = context;
        }

        public async Task<Response<List<UserShortInfo>>> Get(string userId, FollowingStatus status)
        {
            var responce = new Response<List<UserShortInfo>>();

            var followersIds = await _context.Comunities.Where(x => x.FollowingToId == userId && x.Status == status)
                                                        .Select(x=>x.FollowingFromId).ToListAsync();
            
            if (followersIds?.Any() != true)
            {
                responce.Error = new Error("followers can`t be founded by user id");
                return responce;
            }
            
            var users = await _context.Users.Include(x => x.UserInfo)
                                     .Where(q => followersIds.Contains(q.Id))
                                     .Select(x => new UserShortInfo(x))
                                     .ToListAsync();
            
            responce.Data = users;
            return responce;
        }

        public async Task<Response<OperationResult>> Create(string userId, FollowingRequest model)
        {
            var responce = new Response<OperationResult>();

            var entity = new ComunityEntity(userId, model);

            await _context.Comunities.AddAsync(entity);
            await _context.SaveChangesAsync();
            
            responce.Data = OperationResult.Success;
            return responce;
        }

        public async Task<Response<ComunityEntity>> Update(string userId, ComunityEntity entity)
        {
            var responce = new Response<ComunityEntity>();

            var isExist = await _context.Comunities.AnyAsync(x => x == entity);

            if (!isExist)
            {
                responce.Error = new Error("This following not exist");
                return responce;
            }

            if (userId != entity.FollowingFromId || userId != entity.FollowingToId)
            {
                responce.Error = new Error("You don`t have permisions to update this state");
                return responce;
            }

            switch (entity.Status)
            {
                case FollowingStatus.Accepted:
                    _context.Comunities.Update(entity);
                    break;
                case FollowingStatus.Rejected:
                    _context.Comunities.Remove(entity);
                    break;
            }

            await _context.SaveChangesAsync();

            responce.Data = entity;
            return responce;
        }
    }
}
