using ApiClient;
using ApiClient.DTOs;
using ApiClient.Interfaces;
using Artalk.Xmpp.Client;
using Artalk.Xmpp.Im;
using ChatServiceWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServiceWebApplication.Controllers
{
    public class ChatsController : Controller
    {
        /// <summary>
        /// Controller responsible for instantating the XMPP client and handling login and sending messages
        /// </summary>
       
        private string _hostname = "localhost";
        private static string _username;

        //XMPP client
        private static ArtalkXmppClient _xmppClient;

        //Api client for Ejabberd API
        private IApiClient _client;

        //Api client for C# API within this solution
        private MessageClient _messageClient;

        public ChatsController(IApiClient client, MessageClient messageClient)
        {
            _client = client;
            _messageClient = messageClient;
        }

        public IActionResult Index()
        {
            try
            {
                _xmppClient.Message += OnNewMessage;
                _xmppClient.Connect();
                System.Console.WriteLine($"You are logged in as: {_username}");
                return View();
            }
            catch (System.Exception)
            {
                return RedirectToAction("ShowLoginError", "Logins");
            }
        }

        /// <summary>
        /// Opens up a chat Razor page, presenting the message archive and inserting the recipient in the header
        /// </summary>
        /// <param name="jid">The id of the recipient</param>
        /// <returns></returns>
        public async Task<IActionResult> OpenChat(string jid)
        {
            ChatModel chatModel = new();
            chatModel.Message = new () { Recipient = jid };
            List<string> messages = (await _messageClient.GetAllMessagesForUser(_username, jid)).ToList();
            chatModel.Archive = messages;
            return View(chatModel);
        }

        /// <summary>
        /// Fetching the list of users that are available in the system
        /// </summary>
        /// <returns>Returns the view with the list of users</returns>
        public async Task<IActionResult> UsersList()
        {
            IEnumerable<string> usersList = await _client.GetAllUsersAsync();
            return View(usersList);
        }
        
        /// <summary>
        /// Sends the message using the XMPP client instant messaging functionality
        /// </summary>
        /// <param name="chatModel">Model object to pass through multiple values, including the recipient and message</param>
        /// <returns></returns>
        public IActionResult SendMessage(ChatModel chatModel)
        {
            _xmppClient.SendMessage(chatModel.Message.Recipient, chatModel.Message.Content);
            return View("OpenChat", chatModel);
        }

        /// <summary>
        /// Login method validating first through the Ejabberd API, then instantiating the XMPP client with the correct login credentials
        /// </summary>
        /// <param name="loginInfo">Data transfer object to pass through the credentials</param>
        /// <returns></returns>
        public async Task<IActionResult> Login([FromForm] LoginDTO loginInfo)
        {
            //call to the Ejabberd API to validate the user password
            bool response = await _client.LoginAsync(loginInfo);

            if (response)
            {
                _username = loginInfo.Username;
                //instantiating the XMPP client and logging in the user
                _xmppClient = new ArtalkXmppClient(_hostname, _username, loginInfo.Password);
                _xmppClient.Connect();

                return RedirectToAction("UsersList", "Chats");
            }
            else
            {
                ViewBag.ErrorMessage = "Incorrect login";
                return View();
            }
        }

        #region Event handlers

        public void OnNewMessage(object sender, MessageEventArgs e)
        {
            string message = "Message from <" + e.Jid + ">: " + e.Message.Body;
            ViewData["message"] = message;
        }

        #endregion

    }
}
