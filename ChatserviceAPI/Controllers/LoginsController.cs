using ChatserviceAPI.DTOs;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatserviceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LoginsController : ControllerBase
    {
        IUserRepository _userRepository;
        public LoginsController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // POST api/<LoginsController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] LoginDTO login)
        {
            return await _userRepository.LoginAsync(login.Username, login.Password);

        }
    }
}
