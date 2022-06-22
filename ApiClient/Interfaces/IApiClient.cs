using ApiClient.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiClient.Interfaces
{
    public interface IApiClient
    {
        Task<IEnumerable<string>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<int> CreateUserAsync(RegisterDTO userDTO);
        Task<bool> UpdateUserAsync(int id, UserDTO userDTO);
        Task<bool> DeleteUserByIdAsync(int id);
        Task<UserDTO> GetUserByPhoneNumberAsync(string phoneNumber);
        Task<bool> LoginAsync(LoginDTO user);
    }
}
