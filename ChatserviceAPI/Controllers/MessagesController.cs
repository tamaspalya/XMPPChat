using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Repositories;

namespace ChatserviceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        IMessageRepository _messageRepository;
        public MessagesController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        // GET api/<MessagesController>
        [HttpGet("{reciever}/{sender}")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllMessages(string reciever, string sender)
        {
            IEnumerable<string> messages = await _messageRepository.GetAllMessagesForUser(reciever,sender);
            if (messages == null) { return NotFound(); }
            else { return Ok(messages); }
        }
    }
}
