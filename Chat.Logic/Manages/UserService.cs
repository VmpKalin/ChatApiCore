using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Chat.Data.Context;
using Chat.Data.Models.DTO;
using Chat.Data.Models.Entities;
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

        public async Task<bool> CreateUser(UserCreateRequest model)
        {
            if (model == null)
            {
                return false;
            }

            var entity = new UserEntity(model);

            var isExist = _context.Users.Any(x => x.Login.Equals(entity.Login, StringComparison.OrdinalIgnoreCase) || x.Email.Equals(entity.Email, StringComparison.OrdinalIgnoreCase));

            if (isExist)
            {
                return false;
            }

            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<UserEntity> FindUserById(string id)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Id == id); ;
        }

        public Task<List<UserEntity>> GetUsers()
        {
            return _context.Users.ToListAsync();
        }

        public Task<UserEntity> GetUsers(Expression<Func<UserEntity, bool>> predicate)
        {
            return _context.Users.FirstOrDefaultAsync(predicate);
        }

        public Task DeleteUsers()
        {
            _context.Users.RemoveRange(_context.Users);
            return _context.SaveChangesAsync();
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
    }
}
