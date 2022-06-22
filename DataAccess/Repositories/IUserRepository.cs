using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserRepository 
    {
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<bool> UpdateAsync(int id, User user);
        Task<bool> DeleteAsync(int id);
        Task<int> CreateAsync(User entity, string password);
        Task<bool> UpdatePasswordAsync(string email, string oldPassword, string newPassword);
        Task<int> LoginAsync(string email, string password);
        Task<User> GetByPhoneNumberAsync(string phoneNumber);
    }
}
