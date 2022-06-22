using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IMessageRepository
    {
        Task<IEnumerable<string>> GetAllMessagesForUser(string reciever, string sender);
    }
}
