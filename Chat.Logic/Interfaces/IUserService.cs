using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Chat.Data.Models.DTO;
using Chat.Data.Models.Entities;

namespace Chat.Logic.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(UserCreateRequest model);

        Task<UserEntity> FindUserById(string id);

        Task<UserEntity> FindByUsername(string login);

        Task<bool> ValidateCredentials(string username, string password);

        Task<List<UserEntity>> GetUsers();

        Task<UserEntity> GetUsers(Expression<Func<UserEntity,bool>> predicate);

        Task DeleteUsers();
    }
}
