using Dapper;
using DataAccess.Authentication;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DataAccess.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(string connectionstring) : base(connectionstring) { }

        public async Task<int> CreateAsync(User entity, string password)
        {
            try
            {
                var query = "INSERT INTO [Users] (username, password_hash, phone_number, address, jid) OUTPUT INSERTED.Id VALUES (@Username, @PasswordHash, @PhoneNumber, @Address, @Jid);";
                var passwordHash = BCryptTool.HashPassword(password);
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<int>(query, new {Username = entity.Username, PasswordHash = passwordHash, PhoneNumber = entity.PhoneNumber, Address = entity.Address, Jid = entity.Jid });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating new User: '{ex.Message}'.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var query = "DELETE FROM [Users] WHERE Id=@Id";
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(query, new { id }) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting User with id {id}: '{ex.Message}'.", ex);
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                var query = "SELECT * FROM Users";
                using var connection = CreateConnection();
                return (await connection.QueryAsync<User>(query)).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all users: '{ex.Message}'.", ex);
            }
        }

        public async Task<User> GetByPhoneNumberAsync(string phoneNumber)
        {
            try
            {
                var query = "SELECT * FROM [Users] WHERE phone_number=@phoneNumber;";
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<User>(query, new { phoneNumber });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting an author with phone number: '{ex.Message}'.", ex);
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                var query = "SELECT * FROM dbo.[Users] WHERE Id=@Id";
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<User>(query, new { id });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting order with id {id}: '{ex.Message}'.", ex);
            }
        }

        public async Task<int> LoginAsync(string username, string password)
        {
            try
            {
                var query = "SELECT id, password_hash FROM [Users] WHERE username=@Username";
                using var connection = CreateConnection();

                var userTuple = await connection.QuerySingleAsync<UserTuple>(query, new { Username = username });

                if (userTuple != null && BCryptTool.ValidatePassword(password, userTuple.Password_Hash))
                {
                    return userTuple.Id;
                }
                return -1;
            }
            catch (InvalidOperationException)
            {
                return -1;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error logging in for User with username {username}: '{ex.Message}'.", ex);
            }
        }

        public async Task<bool> UpdateAsync(int id, User entity)
        {
            try
            {
                var query = "UPDATE Users SET username = @Username, phone_number = @PhoneNumber, address = @Address WHERE Id=@Id";
                using var connection = CreateConnection();
                entity.Id = id;
                return await connection.ExecuteAsync(query, entity) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting order with id {id}: '{ex.Message}'.", ex);
            }
        }

        public async Task<bool> UpdatePasswordAsync(string username, string oldPassword, string newPassword)
        {
            try
            {
                var query = "UPDATE Users SET HashPassword=@NewHashPassword WHERE Id=@Id;";
                var id = await LoginAsync(username, oldPassword);
                if (id > 0)
                {
                    var newHashPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                    using var connection = CreateConnection();
                    return await connection.ExecuteAsync(query, new { Id = id, NewHashPassword = newHashPassword }) > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating password: '{ex.Message}'.", ex);
            }
        }

    }
    internal class UserTuple
    {
        public int Id;
        public string Password_Hash;
    }
}
