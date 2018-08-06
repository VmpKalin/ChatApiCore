using Chat.Data.Context;
using Chat.Data.Interfaces.IEntities;
using Chat.Data.Models.DTO;
using Chat.Data.Models.Entities;
using Chat.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Logic.Manages
{
    public class ChatService : IChatService
    {
        private readonly GeneralContext _context;

        public ChatService(GeneralContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveMessageAsync(MessageDTO message)
        {
            var userFrom = await _context.Users.FirstOrDefaultAsync(x=>x.Id == message.UserIdFrom);
            var userTo = await _context.Users.FirstOrDefaultAsync(x=>x.Id == message.UserIdTo);
            var room = await _context.ChatRooms.FirstOrDefaultAsync(x => x.Id == message.RoomId);
            
            if(userFrom == null || userTo == null)
            {
                throw new NullReferenceException();
            }

            if(room == null)
            {
                var roomEntity = new RoomEntity(userFrom, userTo, message.Data);
                await _context.ChatRooms.AddAsync(roomEntity);
                await _context.SaveChangesAsync();
            }

            if(room!=null)
                if (room.UserFrom.Id != userFrom.Id || room.UserTo.Id != userTo.Id)
                {
                    throw new NullReferenceException();
                }

            var messageEntity = new MessageEntity(message.Data,message.Time,room);

            await _context.Messages.AddAsync(messageEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        //public Task<TResult> GetModelOrDefaultVal<T,TResult>(T list, string id) where T : IQueryable<TResult> 
        //                                                          where TResult : IEntity<string>
        //{
        //    return list.FirstOrDefaultAsync(x => x.Id == id);
        //}
    }
}
