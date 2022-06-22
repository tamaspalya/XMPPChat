using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public MessageRepository(string connectionstring) : base(connectionstring) { }

        /// <summary>
        /// Retrieves all the messages for a conversation of two specific users
        /// </summary>
        /// <param name="username">The logged in user</param>
        /// <param name="bare_peer">The recipient in the chat</param>
        /// <returns>Returns a list of string values representing each message in the conversation</returns>
        public async Task<IEnumerable<string>> GetAllMessagesForUser(string username, string bare_peer)
        {
            try
            {
                var query = "SELECT txt FROM dbo.[archive] WHERE username=@username AND bare_peer=@bare_peer";
                using var connection = CreateConnection();
                return (await connection.QueryAsync<string>(query, new { username, bare_peer })).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting messages for a user {username}: '{ex.Message}'.", ex);
            }
        }
    }
}
