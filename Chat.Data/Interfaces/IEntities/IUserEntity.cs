using System.Collections.Generic;

namespace Chat.Data.Interfaces.IEntities
{
    public interface IUserEntity 
    {
        int Age { get; set; }
        string Email { get; set; }
        string FName { get; set; }
        string LName { get; set; }
    }
}