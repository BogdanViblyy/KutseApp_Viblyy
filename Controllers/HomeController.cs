using KutseApp_Viblyy.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using KutseApp_Viblyy.Data;

namespace KutseApp_Viblyy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            string greeting;

            var hour = DateTime.Now.Hour;
            if (hour < 12)
                greeting = "Доброе утро";
            else if (hour < 18)
                greeting = "Добрый день";
            else
                greeting = "Добрый вечер";

            ViewBag.Greeting = greeting;

            var currentMonth = DateTime.Now.Month;
            var holiday = _context.Holidays.FirstOrDefault(h => h.Date.Month == currentMonth);

            return View(holiday);
        }

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
