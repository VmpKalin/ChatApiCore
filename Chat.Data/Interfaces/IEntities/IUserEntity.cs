using System.Collections.Generic;

namespace Chat.Data.Interfaces.IEntities
{
    public interface IUserEntity 
    {
        int Age { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        List<string> Friends { get; set; }
    }
}