using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Chat.Data.Models.AdditionModels;
using Chat.Data.Models.DTO;
using Chat.Data.Models.Entities;
using Chat.Data.Models.Entities.UserModels;

namespace Chat.Logic.Interfaces
{
    public interface IUserService
    {
        Task<Responce<OperationResult>> CreateUser(UserCreationRequest model);

        Task<Responce<OperationResult>> AddUserInfo(string userId, UserInfoDTO model);

        Task<Responce<UserEntity>> FindUserById(string id);

        Task<UserEntity> FindByUsername(string login);

        Task<bool> ValidateCredentials(string username, string password);

        Task<Responce<List<UserEntity>>> GetUsers();

        Task<Responce<UserEntity>> GetUser(Expression<Func<UserEntity,bool>> predicate);

        Task EraseDb();
    }
}
