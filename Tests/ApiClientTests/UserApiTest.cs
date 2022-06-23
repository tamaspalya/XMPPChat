using ApiClient;
using ApiClient.DTOs;
using ApiClient.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests.ApiClientTests
{
    class UserApiTest
    {
        IApiClient _client;


        UserDTO _user;
        string _password;

        [SetUp]
        public async Task SetUp()
        {
            _client = new Client(Configuration.LOCAL_URI);
            _user = new UserDTO() { Username = "Test", Address = "tst", PhoneNumber = "1234", Jid = "hej@test" };
            _password = "usertest1";
            RegisterDTO registration = new RegisterDTO() { Username = _user.Username, Address = _user.Address, PhoneNumber = _user.PhoneNumber, Jid = _user.Jid, Password = _password };
            _user.Id = await _client.CreateUserAsync(registration);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _client.DeleteUserByIdAsync(_user.Id);

        }

        [Test]
        public async Task GetAllUsersAsync()
        {
            //arrange
            //act
            IEnumerable<string> users = await _client.GetAllUsersAsync();
            //assert
            Assert.IsNotNull(users);
        }
        
        
        [Test]
        public async Task GetUserByPhoneNumber()
        {
            //arrange
            string phoneNumber = _user.PhoneNumber;
            //act
            UserDTO user = await _client.GetUserByPhoneNumberAsync(phoneNumber);
            //assert
            Assert.IsNotNull(user);

        }
        
        
        [Test]
        public async Task GetUserByIdAsync()
        {
            //arrange
            int id = _user.Id;
            //act
            UserDTO user = await _client.GetUserByIdAsync(id);
            //assert
            Assert.IsNotNull(user);

        }
        
        /*
        [Test]
        public async Task LoginAsync()
        {
            //arrange
            LoginDto login = new LoginDto() { Email = _user.Email, Password = _password };
            //act
            int id = await _client.LoginAsync(login);
            //assert
            Assert.IsTrue(id == _user.Id);
        }
        
        */

        [Test]
        public async Task UpdateUserAsync()
        {
            //arrange
            UserDTO newuser = new UserDTO() { Id = _user.Id, Username = "newname", PhoneNumber = "12345", Address = "Addd", Jid = "1" };
            //act
            bool successful = await _client.UpdateUserAsync(_user.Id, newuser);
            //assert
            Assert.IsTrue(successful);

        }
        
    }
}
