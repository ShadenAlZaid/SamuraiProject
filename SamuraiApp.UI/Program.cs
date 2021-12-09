using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Linq;

namespace SamuraiApp.UI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        private static void Main(string[] args)
        {
            _context.Database.EnsureCreated();
            AddSamuraiByName("Shaden", "Yasser", "Ibrahim", "Samo");
            AddVariousTypes();
            GetSamurais();
            Console.Write("Press any key...");
            Console.ReadKey();
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
    }
}
