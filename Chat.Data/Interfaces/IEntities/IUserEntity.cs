using System.Collections.Generic;

namespace Chat.Data.Interfaces.IEntities
{
    public interface IUserEntity 
    {
        string Email { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string Login { get; set; }
    }
}