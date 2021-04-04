using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Models
{
    public class ApplicationContext : DbContext
    {
        //Specify the path to the database - this is a default location we havent specified a specific path
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;DataBase=BlueFinDB1;Trusted_Connection=False;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(connectionString);
        }

        //We need the Dbset property to maniputlate the entries in the models
        public DbSet<Livestock> Livestocks { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
      
        public DbSet<Order> Orders { get; set; }

        //Seeding to the DB method
        public void SeedDB()
        {
            ApplicationContext db = new ApplicationContext();
            db.Database.EnsureCreated();
            if (db.Livestocks.Count() == 0)
            {
                List<Livestock> LivestockList = new List<Livestock>()
                {
                    new Livestock(CareLevel.Easy, Temperment.Peaceful, WaterType.Fresh, "Black, Silver, Red", "PH:6.0-6.5, KH 0-10, 22°C-26°C", "5cm", "Harlequin Rasbora", "The Harlequin Rasbora is easily identified by its characteristic black pork chop shaped patch and beautifully lustrous copper/orange body", 2.99),
                    new Livestock(CareLevel.Easy, Temperment.Aggressive, WaterType.Fresh, "Black, Blue, Red", "PH:6.0-6.5, KH 0-10, 22°C-26°C", "7.5cm", "Crown Tail Betta", "The Crown Tail Betta has a striking, elaborate tail that differentiates it from other Bettas. The Crown Tail has a teardrop shape to its tail while the Twin Tail is split, almost giving the suggestion of having two tails.", 19.99)
                };

                foreach(Livestock l in LivestockList)
                {
                    db.Livestocks.Add(l);
                }

                List<Equipment> EquipmentList = new List<Equipment>()
                {
                    new Equipment("Juwel", 92, 41, 55, "Black", "50 kg", "Juwel Vision 180", "Painstaking workmanship from Germany, top - quality materials and perfectly tuned technology guarantee the very best of quality and safety, meaning a long service life for your new aquarium.", 610.99),
                    new Equipment("Juwel", 45, 45, 45, "Black", "50 kg", "Juwel Cube 45", "Great Beginnner tank which wont take up much space, great quality for a great price", 200.00)
                };

                foreach (Equipment e in EquipmentList)
                {
                    db.Equipments.Add(e);
                }
                db.SaveChanges();
            }

        }

    }

  
  




}
