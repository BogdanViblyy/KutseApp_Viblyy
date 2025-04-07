using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KutseApp_Viblyy.Data;
using KutseApp_Viblyy.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace KutseApp_Viblyy.Controllers;


[Authorize]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index() => View();

    [HttpPost]
    public IActionResult SendReminder(string email, string name, string title)
    {
        if (string.IsNullOrEmpty(email)) return BadRequest("Email не указан");

        var message = new MailMessage("kutseapp@gmail.com", email)
        {
            Subject = "Напоминание о празднике",
            Body = $"Привет, {name}!\n\nЖдём тебя на празднике: {title}.\nДо встречи!"
        };

        var smtp = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("kutseapp@gmail.com", "itig faxk voqq aahw"),
            EnableSsl = true
        };

        try
        {
            smtp.Send(message);
        }
        catch (Exception ex)
        {
            // Можно логировать или возвращать сообщение об ошибке
            return Content("Ошибка при отправке: " + ex.Message);
        }

        TempData["Message"] = $"Напоминание отправлено на {email}";
        return RedirectToAction("Guests");
    }

    public async Task<IActionResult> Guests()
    {
        var guests = await _context.GuestResponses
            .Include(g => g.Holiday)
            .ToListAsync();
        return View(guests);
    }

    public async Task<IActionResult> Holidays()
    {
        var holidays = await _context.Holidays.ToListAsync();
        return View(holidays);
    }

    [HttpGet]
    public IActionResult AddHoliday() => View();

    [HttpPost]
    public async Task<IActionResult> AddHoliday(Holiday holiday)
    {
        _context.Holidays.Add(holiday);
        await _context.SaveChangesAsync();
        return RedirectToAction("Holidays");
    }

    [HttpGet]
    public async Task<IActionResult> EditHoliday(int id)
    {
        var h = await _context.Holidays.FindAsync(id);
        return View(h);
    }

    [HttpPost]
    public async Task<IActionResult> EditHoliday(Holiday holiday)
    {
        _context.Holidays.Update(holiday);
        await _context.SaveChangesAsync();
        return RedirectToAction("Holidays");
    }

    public async Task<IActionResult> DeleteHoliday(int id)
    {
        var h = await _context.Holidays.FindAsync(id);
        _context.Holidays.Remove(h);
        await _context.SaveChangesAsync();
        return RedirectToAction("Holidays");
    }

    public async Task<IActionResult> DeleteGuest(int id)
    {
        var g = await _context.GuestResponses.FindAsync(id);
        _context.GuestResponses.Remove(g);
        await _context.SaveChangesAsync();
        return RedirectToAction("Guests");
    }
    [HttpGet]
    public async Task<IActionResult> EditGuest(int id)
    {
        var guest = await _context.GuestResponses.Include(g => g.Holiday).FirstOrDefaultAsync(g => g.Id == id);
        if (guest == null) return NotFound();

        var holidays = await _context.Holidays.ToListAsync();
        ViewBag.Holidays = new SelectList(holidays, "Id", "Title", guest.HolidayId);
        return View(guest);
    }

    [HttpPost]
    public async Task<IActionResult> EditGuest(GuestResponse guest)
    {
        _context.GuestResponses.Update(guest);
        await _context.SaveChangesAsync();
        return RedirectToAction("Guests");
    }

    [HttpGet]
    public async Task<IActionResult> AddGuest()
    {
        var holidays = await _context.Holidays.ToListAsync();
        ViewBag.Holidays = new SelectList(holidays, "Id", "Title");
        return View(new GuestResponse());
    }

    [HttpPost]
    public async Task<IActionResult> AddGuest(GuestResponse guest)
    {
        guest.Response = "Приду"; // По умолчанию
        _context.GuestResponses.Add(guest);
        await _context.SaveChangesAsync();
        return RedirectToAction("Guests");
    }
}
