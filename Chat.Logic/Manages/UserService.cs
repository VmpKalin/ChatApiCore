using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Chat.Data.Context;
using Chat.Data.Models.AdditionModels;
using Chat.Data.Models.DTO;
using Chat.Data.Models.Entities;
using Chat.Data.Models.Entities.UserModels;
using Chat.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.Logic.Manages
{
    public class UserService : IUserService
    {
        private readonly GeneralContext _context;

        public UserService(GeneralContext context)
        {
            _context = context;
        }

        public async Task<Response<OperationResult>> CreateUser(UserCreationRequest model)
        {
            var responce = new Response<OperationResult>
            {
                Data = OperationResult.Failed
            };

            if (model == null)
            {
                return responce;
            }

            var entity = new UserEntity(model);

            var isExist = _context.Users.Any(x => x.Login.Equals(entity.Login, StringComparison.OrdinalIgnoreCase) || x.Email.Equals(entity.Email, StringComparison.OrdinalIgnoreCase));

            if (isExist)
            {
                responce.Error = new Error("User info is created!");
                return responce;
            }

            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();

            responce.Data = OperationResult.Success;
            return responce;
        }

        public async Task<Response<UserEntity>> FindUserById(string id)
        {
            var responce = new Response<UserEntity>();

            responce.Data = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (responce.Data == null)
            {
                responce.Error = new Error("User can`t be found by this id");
            }
            
            return responce;
        }

        public async Task<Response<List<UserEntity>>> GetUsers()
        {
            var responce = new Response<List<UserEntity>>();

            responce.Data = await _context.Users.Include(x=>x.Posts).Include(x=>x.UserInfo).ToListAsync();

            return responce;
        }

        public async Task<Response<UserEntity>> GetUser(Expression<Func<UserEntity, bool>> predicate)
        {
            var responce = new Response<UserEntity>();

            responce.Data = await _context.Users.Include(x => x.Posts).Include(x => x.UserInfo).FirstOrDefaultAsync(predicate);

            return responce;
        }

        public async Task EraseDb()
        {
            var tableNames = new List<string>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE '%Migration%'";
                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        tableNames.Add(reader[0].ToString() + " " + reader[1].ToString());
                    }
                }
            }

            foreach (var table in tableNames)
            {
                try
                {
                    var result = _context.Database.ExecuteSqlCommand(string.Format("TRUNCATE TABLE [{0}]", table));
                    if (result == 1)
                        break;

                }
                catch (Exception ex)
                {

                }
            }

        }

        public async Task<bool> ValidateCredentials(string username, string password)
        {
            var user = await FindByUsername(username);
            
            if (user != null)
            {
                return user.Password.Equals(password);
            }

            return false;
        }

        public Task<UserEntity> FindByUsername(string login)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Login.Equals(login, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Response<OperationResult>> AddUserInfo(string userId, UserInfoDTO model)
        {
            var responce = new Response<OperationResult>()
            {
                Data = OperationResult.Failed
            };
            
            var isExist = await _context.Users.AnyAsync(x => x.Id == userId);

            if (!isExist)
            {
                responce.Error = new Error("User can`t be founded by user id");
                return responce;
            }

            var infoEnity = new UserInfoEntity(model, userId);

            await _context.UsersInfo.AddAsync(infoEnity);
            await _context.SaveChangesAsync();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            user.UserInfo = infoEnity;

            _context.Update(user);
            await _context.SaveChangesAsync();

            responce.Data = OperationResult.Success;
            return responce;
        }
    }
}
