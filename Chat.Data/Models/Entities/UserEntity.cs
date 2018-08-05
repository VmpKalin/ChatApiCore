using Chat.Data.Interfaces;
using Chat.Data.Interfaces.IEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Chat.Data.Models.DTO;

namespace Chat.Data.Models.Entities
{
    public class UserEntity : IEntity<string>, IUserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Login { get; set; }

        public int Age { get; set; }

        public UserEntity(UserCreateRequest model)
        {
            FName = model.FName;
            LName = model.LName;
            Email = model.Email;
            Password = model.Password;
            Login = model.Login;
            Age = model.Age;
        }

        public UserEntity()
        {

        }

        public UserEntity(string fName, string lName, string email, string password, string login, int age)
        {
            FName = fName;
            LName = lName;
            Email = email;
            Password = password;
            Login = login;
            Age = age;
        }
    }
}
