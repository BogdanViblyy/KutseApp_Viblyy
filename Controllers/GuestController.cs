using Microsoft.AspNetCore.Mvc;
using KutseApp_Viblyy.Data;
using System.Net.Mail;
using System.Net;
using KutseApp_Viblyy.Models;
using System;

namespace KutseApp_Viblyy.Controllers;

    public class GuestController : Controller
{
    private readonly ApplicationDbContext _context;

    public GuestController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Respond(int holidayId)
    {
        var holiday = _context.Holidays.Find(holidayId);
        if (holiday == null) return NotFound();

        var viewModel = new GuestResponseViewModel
        {
            HolidayId = holiday.Id,
            HolidayTitle = holiday.Title,
            HolidayDate = holiday.Date,
            HolidayAddress = holiday.Address
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Respond(GuestResponseViewModel model)
    {
        if (model.Response == "Приду")
        {
            if (!ModelState.IsValid) return View(model);

            var guest = new GuestResponse
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Response = model.Response,
                HolidayId = model.HolidayId
            };

            _context.GuestResponses.Add(guest);
            _context.SaveChanges();

            TempData["GuestEmail"] = model.Email;
            TempData["GuestName"] = model.FirstName;
            TempData["HolidayTitle"] = model.HolidayTitle;

            return RedirectToAction("Coming");
        }
        else if (model.Response == "Еще подумаю")
        {
            var holiday = _context.Holidays.Find(model.HolidayId);
            if (holiday != null)
            {
                holiday.ThinkingCount++;
                _context.SaveChanges();
            }

            return RedirectToAction("Thinking");
        }

        return View(model);
    }


    public IActionResult Coming()
    {
        return View();
    }

    public IActionResult Thinking()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SendReminder()
    {
        var email = TempData["GuestEmail"]?.ToString();
        var name = TempData["GuestName"]?.ToString();
        var title = TempData["HolidayTitle"]?.ToString();

        if (string.IsNullOrEmpty(email)) return BadRequest();

        var message = new MailMessage("your@email.com", email)
        {
            Subject = "Напоминание о празднике",
            Body = $"Привет, {name}!\n\nЖдем тебя на празднике: {title}.\nДо встречи!"
        };

        var smtp = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("kutseapp@gmail.com", "itig faxk voqq aahw"),
            EnableSsl = true
        };

        smtp.Send(message);

        return RedirectToAction("Thanks");
    }

    public IActionResult Thanks()
    {
        return View();
    }
}
