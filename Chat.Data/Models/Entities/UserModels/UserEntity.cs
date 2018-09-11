using Chat.Data.Interfaces;
using Chat.Data.Interfaces.IEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Chat.Data.Models.DTO;
using Chat.Data.Models.Entities.Comunity;

namespace Chat.Data.Models.Entities.UserModels
{
    public class UserEntity : IEntity<string>, IUserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserInfoEntity UserInfo { get; set; }

        public ICollection<PostEntity> Posts { get; set; } = new List<PostEntity>();

        public UserEntity(UserCreationRequest model)
        {
            FirstName = model.FName;
            LastName = model.LName;
            Email = model.Email;
            Password = model.Password;
            Login = model.Login;
        }

        public UserEntity()
        {

        }

        public UserEntity(string fName, string lName, string email, string password, string login, int age)
        {
            FirstName = fName;
            LastName = lName;
            Email = email;
            Password = password;
            Login = login;
        }
    }
}
