using KutseApp_Viblyy.Data;
using KutseApp_Viblyy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace KutseApp_Viblyy.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Проверка: есть ли уже данные
                if (context.Holidays.Any())
                {
                    return; // Данные уже были добавлены
                }
                context.Holidays.AddRange(
                    new Holiday
                    {
                        Title = "Зимний карнавал",
                        Description = "Яркое зимнее шоу с музыкой и костюмами!",
                        Date = new DateTime(DateTime.Now.Year, 1, 20),
                        Address = "г. Таллинн, пл. Свободы 1"
                    },
                    new Holiday
                    {
                        Title = "День влюбленных",
                        Description = "Праздник для всех сердец.",
                        Date = new DateTime(DateTime.Now.Year, 2, 14),
                        Address = "г. Тарту, Ратушная площадь"
                    },
                    new Holiday
                    {
                        Title = "Весеннее пробуждение",
                        Description = "Первая прогулка по парку в этом году.",
                        Date = new DateTime(DateTime.Now.Year, 3, 10),
                        Address = "г. Пярну, Центральный парк"
                    },
                    new Holiday
                    {
                        Title = "Весенний фестиваль",
                        Description = "Праздник весны для всех друзей!",
                        Date = new DateTime(DateTime.Now.Year, 4, 15),
                        Address = "г. Таллинн, ул. Цветочная 5"
                    },
                    new Holiday
                    {
                        Title = "Майский пикник",
                        Description = "Собираемся всей компанией на природе!",
                        Date = new DateTime(DateTime.Now.Year, 5, 18),
                        Address = "г. Вильянди, Лесной луг"
                    },
                    new Holiday
                    {
                        Title = "День музыки",
                        Description = "Концерт под открытым небом.",
                        Date = new DateTime(DateTime.Now.Year, 6, 21),
                        Address = "г. Таллинн, Музыкальный парк"
                    },
                    new Holiday
                    {
                        Title = "Летний фестиваль света",
                        Description = "Шоу фонарей и фейерверков.",
                        Date = new DateTime(DateTime.Now.Year, 7, 14),
                        Address = "г. Нарва, набережная"
                    },
                    new Holiday
                    {
                        Title = "Августовская ярмарка",
                        Description = "Ярмарка местных продуктов и ремесел.",
                        Date = new DateTime(DateTime.Now.Year, 8, 12),
                        Address = "г. Хаапсалу, центр"
                    },
                    new Holiday
                    {
                        Title = "День знаний",
                        Description = "Праздничное открытие учебного года.",
                        Date = new DateTime(DateTime.Now.Year, 9, 1),
                        Address = "г. Таллинн, Школьная улица"
                    },
                    new Holiday
                    {
                        Title = "Осенний бал",
                        Description = "Бал в старинном стиле для всех желающих.",
                        Date = new DateTime(DateTime.Now.Year, 10, 10),
                        Address = "г. Раквере, городской зал"
                    },
                    new Holiday
                    {
                        Title = "Праздник урожая",
                        Description = "День сбора урожая и угощений.",
                        Date = new DateTime(DateTime.Now.Year, 11, 5),
                        Address = "г. Пайде, центральная площадь"
                    },
                    new Holiday
                    {
                        Title = "Рождественский вечер",
                        Description = "Сказочное завершение года с подарками.",
                        Date = new DateTime(DateTime.Now.Year, 12, 24),
                        Address = "г. Таллинн, Ратушная площадь"
                    }
                );


                context.SaveChanges();
            }
        }
    }
}
