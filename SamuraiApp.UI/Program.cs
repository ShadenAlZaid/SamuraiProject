using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SamuraiApp.UI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();
        private static SamuraiContext _contextNT = new SamuraiContext();

        private static void Main(string[] args)
        {
            _context.Database.EnsureCreated();
            //AddSamuraiByName("Shaden", "Yasser", "Ibrahim", "Samo");
            AddVariousTypes();
            //GetSamurais();
            //Console.Write("Press any key...");
            //Console.ReadKey();
            QueryAndUpdateBattles_Disconnected();
        }

        private static void AddSamuraiByName(params string[] names)
        {
            foreach (string name in names)
            {
                _context.Samurais.Add(new Samurai { Name = name });
            }
            _context.SaveChanges();
        }
        private static void GetSamurais()
        {
            var samurais = _context.Samurais
            .TagWith("ConsoleApp.Program.GetSamurais method")
            .ToList();

            Console.WriteLine($"Samurai count is {samurais.Count}");

            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void AddVariousTypes()
        {
            _context.AddRange(
                new Samurai { Name = "Sadeem" },
                new Samurai { Name = "Rahaf" },
                new Battle { Name = "Battle of Anegwa" },
                new Battle { Name = "Battle of Nagashino" });
            _context.SaveChanges();
        }

        private static void QueryAndUpdateBattles_Disconnected()
        {
            List<Battle> disconnectedBattles;

            using (var context1 = new SamuraiContext())
            {
                disconnectedBattles = _context.Battles.ToList();
            }
            disconnectedBattles.ForEach(b =>
            {
                b.StartDate = new DateTime(1570, 01, 01);
                b.EndDate = new DateTime(1570, 12, 01);
            });

            using (var context2 = new SamuraiContext())
            {
                context2.UpdateRange(disconnectedBattles);
                context2.SaveChanges();
            }
        }

        private static void InsertSamuraiWithQuote()
        {
            var samurai = new Samurai
            {
                Name = "Shado",
                Quotes = new List<Quote> {
                    new Quote{ Text = "Hello" }
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void InsertQuoteToExistingSamuraiNotTracked()
        {
            var samurai = _context.Samurais.Find(Id);
            samurai.Quotes.Add(new Quote { Text = "Greetings" });
            
            using (var newContext = new SamuraiContext())
            {
                newContext.Samurais.Update(samurai);
                newContext.SaveChanges();
            }
        }
    }
}
