using ApiClient.DTOs;
using ApiClient.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ApiClient
{
    public class Client : IApiClient
    {
        private RestClient _restClient;
        public Client(string uri) => _restClient = new RestClient(new Uri(uri));


        #region CRUD functionality
        public async Task<int> CreateUserAsync(RegisterDTO userDTO)
        {
            var response = await _restClient.RequestAsync<int>(Method.POST, "users", userDTO);
            if (!response.IsSuccessful)
            {
                throw new Exception($"Error creating user with username={userDTO.Username}. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<bool> DeleteUserByIdAsync(int id)
        {
            var response = await _restClient.RequestAsync(Method.DELETE, $"users/{id}", null);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                throw new Exception($"Error deleting user with id={id}. Message was {response.Content}");
            }
        }

        public async Task<IEnumerable<string>> GetAllUsersAsync()
        {
            var request = new RestRequest(Method.POST);
            request.Resource = $"registered_users";
            request.AddJsonBody(new {host = "localhost"});

            var response = await _restClient.ExecuteAsync<IEnumerable<string>>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving all users. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var response = await _restClient.RequestAsync<UserDTO>(Method.GET, $"users/{id}");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving user by id: {id}. Message was {response.Content}");
            }
            return response.Data;
        }

        public async Task<UserDTO> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            var response = await _restClient.RequestAsync<UserDTO>(Method.GET, $"users/phone/{phoneNumber}");

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error retrieving user by phone number: {phoneNumber}. Message was {response.Content}");
            }
            return response.Data;
        }


        public async Task<bool> UpdateUserAsync(int id, UserDTO userDTO)
        {
            var response = await _restClient.RequestAsync(Method.PUT, $"users/{id}", userDTO);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                throw new Exception($"Error updating user with username={userDTO.Username}. Message was {response.Content}");
            }

        }

        #endregion

        /* Login with our own API - Old code
        public async Task<int> LoginAsync(LoginDTO user)
        {
            var request = new RestRequest(Method.POST);
            request.Resource = $"logins";
            request.RequestFormat = DataFormat.Json;
            request.AddBody(user);
            var response = await _restClient.ExecuteAsync<int>(request);
            // var response = await _restClient.RequestAsync<int>(Method.POST, $"logins", user);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error logging in for user with username={user.Username}. Message was {response.Content}");
            }
            return response.Data;
        }
        */

        public async Task<bool> LoginAsync(LoginDTO user)
        {


            var request = new RestRequest(Method.POST);
            request.Resource = $"check_password";
            XmppLoginDto loginDto = new() { host = "localhost", password = user.Password, user = user.Username };
            request.AddJsonBody(loginDto);

            var response = await _restClient.ExecuteAsync<int>(request);
            // var response = await _restClient.RequestAsync<int>(Method.POST, $"logins", user);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error logging in for user with username={user.Username}. Message was {response.Content}");
            }
            return response.IsSuccessful;

        }

        

    }
   
}

