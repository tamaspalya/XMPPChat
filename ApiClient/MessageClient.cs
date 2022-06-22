using ApiClient.DTOs;
using ApiClient.Interfaces;
using Artalk.Xmpp.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ApiClient
{
    /// <summary>
    /// MessageClient calls the C# Api to access the database
    /// </summary>
    public class MessageClient
    {
        private RestClient _restClient;
        public MessageClient(string uri) => _restClient = new RestClient(new Uri(uri));

        public async Task<IEnumerable<string>> GetAllMessagesForUser(string reciever, string sender)
        {
            var request = new RestRequest(Method.GET);
            request.Resource = $"Messages/{reciever}/{sender}";

            var response = await _restClient.ExecuteAsync<IEnumerable<string>>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all messages. Message was {response.Content}");
            }
            return response.Data;
        }

    }
   
}

