using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UML.Models;

namespace UML.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Open Home Page
        public IActionResult Index()
        {
            return View();
        }

        //Open CLI Download Page
        public IActionResult CLIDownload()
        {
            return View();
        }

        //Open Help Page
        public IActionResult Help()
        {
            return View();
        }

        //Open Privacy Policy Page
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}