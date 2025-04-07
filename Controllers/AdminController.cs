using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KutseApp_Viblyy.Data;
using KutseApp_Viblyy.Models;

using Microsoft.EntityFrameworkCore;
using System;


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
}
