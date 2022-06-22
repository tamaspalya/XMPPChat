using ChatserviceAPI.DTOs;
using ChatserviceAPI.DTOs.Converters;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatserviceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users.ToList().ToDtos());
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) { return NotFound(); }
            else { return Ok(user.ToDTO()); }
        }

        [HttpGet("phone/{phoneNumber}")]
        //[Route("phone")]
        public async Task<ActionResult<UserDTO>> Get(string phoneNumber)
        {
            var user = await _userRepository.GetByPhoneNumberAsync(phoneNumber);
            if (user == null) { return NotFound(); }
            else { return Ok(user.ToDTO()); }
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] RegisterDTO userDTO)
        {
            return Ok(await _userRepository.CreateAsync(new User {  Address = userDTO.Address, Jid = userDTO.Jid, PhoneNumber = userDTO.PhoneNumber, Username = userDTO.Username}, userDTO.Password));
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] UserDTO userDTO)
        {
            bool wasUpdated =  await _userRepository.UpdateAsync(id, userDTO.FromDTO());
            if (!wasUpdated)
            {
                return NotFound();
            }
                return Ok();      
            }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            bool wasDeleted = await _userRepository.DeleteAsync(id);
            if (!wasDeleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
    
}
