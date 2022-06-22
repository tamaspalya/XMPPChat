using ApiClient.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatServiceWebApplication.Controllers
{
    public class LoginsController : Controller
    {
        IApiClient _client;
        public LoginsController(IApiClient client)
        {
            _client = client;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginForm()
        {
            return View();
        }

        public IActionResult ShowLoginError()
        {
            ViewBag.ErrorMessage = "Incorrect login";
            return View("LoginForm");
        }
    }
}
