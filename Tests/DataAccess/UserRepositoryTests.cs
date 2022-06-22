using DataAccess.Models;
using DataAccess.Repositories;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.DataAccess
{
    class UserRepositoryTests
    {
        public IUserRepository _userRepository;

        private User _user;
        private string _password;

        [SetUp]
        public async Task Setup()
        {
            _userRepository = new UserRepository(Configuration.CONNECTION_STRING);
            _user = new() { Username = "test", PhoneNumber = "1111", Address = "testaddress", Jid = "test@test" };
            _password = "testpass";
            _user.Id = await _userRepository.CreateAsync(_user, _password);
        }

        [TearDown]
        public async Task Cleanup()
        {
            await _userRepository.DeleteAsync(_user.Id);
        }

        [Test]
        public async Task GetAllUsers()
        {
            //arrange
            //act
            IEnumerable<User> users = await _userRepository.GetAllAsync();
            //assert
            Assert.IsTrue(users.ToList().Count > 0);

        }

        [Test]
        public async Task GetUserById()
        {
            //arrange
            int id = _user.Id;
            //act
            User user = await _userRepository.GetByIdAsync(id);
            //assert
            Assert.AreEqual(_user.Jid, user.Jid, "The retrieved user jids did not match.");
        }

        [Test]
        public async Task GetUserByPhoneNumber()
        {
            //arrange
            string phoneNumber = _user.PhoneNumber;
            //act
            User user = await _userRepository.GetByPhoneNumberAsync(phoneNumber);
            //assert
            Assert.AreEqual(_user.Id, user.Id, "The retrieved user did not match the expected.");
        }

        [Test]
        public async Task UpdateUserById()
        {
            //arrange
            User newUser = new() { Username = "newusername", Address = "newaddress", PhoneNumber = "9999" };
            //act
            bool wasUpdated = await _userRepository.UpdateAsync(_user.Id, newUser);
            //assert
            Assert.IsTrue(wasUpdated, $"User with id: {_user.Id} was not updated!");
        }

        [Test]
        public async Task LoginUserAsync()
        {
            //arrange
            string username = "test";
            string password = "testpass";
            //act
            int userId = await _userRepository.LoginAsync(username, password);
            //assert
            Assert.AreEqual(_user.Id, userId, $"Error logging in with username: {username}");
        }        

    }
}
