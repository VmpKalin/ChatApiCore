using System.Threading.Tasks;
using Chat.Data.Models.DTO;

namespace Chat.Logic.Interfaces
{
    public interface IChatService
    {
        Task<bool> SaveMessageAsync(MessageDTO message);
    }
}