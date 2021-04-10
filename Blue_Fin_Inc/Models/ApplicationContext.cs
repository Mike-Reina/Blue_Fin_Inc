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
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;DataBase=BlueFinDB5;Trusted_Connection=False;";

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
            db.Database.Migrate();
            if (db.Livestocks.Count() == 0)
            {
                List<Livestock> LivestockList = new List<Livestock>()
                {
                    new Livestock(CareLevel.Easy, Temperment.Peaceful, WaterType.Fresh, "Black, Silver, Red", "PH:6.0-6.5, KH 0-10, 22°C-26°C", "5cm", "Harlequin Rasbora", "The Harlequin Rasbora is easily identified by its characteristic black pork chop shaped patch and beautifully lustrous copper/orange body", 2.99){ ImageName = "harlequin-rasbora1.jpg", Stock = 15},
                    new Livestock(CareLevel.Easy, Temperment.Aggressive, WaterType.Fresh, "Black, Blue, Red", "PH:6.0-6.5, KH 0-10, 22°C-26°C", "7.5cm", "Crown Tail Betta", "The Crown Tail Betta has a striking, elaborate tail that differentiates it from other Bettas. The Crown Tail has a teardrop shape to its tail while the Twin Tail is split, almost giving the suggestion of having two tails.", 19.99){ ImageName = "Crowntail_Betta_Red.jpg", Stock = 5}
                };

                foreach (Livestock l in LivestockList)
                {
                    db.Livestocks.Add(l);
                }

                List<Equipment> EquipmentList = new List<Equipment>()
                {
                    new Equipment("Juwel", 92, 41, 55, "Black, Oak, White", "50 kg", "Juwel Vision 180", "Painstaking workmanship from Germany, top - quality materials and perfectly tuned technology guarantee the very best of quality and safety, meaning a long service life for your new aquarium.", 610.99){ ImageName = "large-Rio_125_black_SBX_Combi.jpg", Stock = 20},
                    new Equipment("Fluval", 121, 46, 70, "Black with Oak stand", "65 kg", "Fluval Vicenza 260", "With its graceful curves, the Fluval Vicenza bow-fronted aquarium adds a new dimension to flat walls. With a choice of two sizes, the Vicenza brings designer style to the craft of home aquatics.", 925.00){ ImageName = "large-Fluval-Vicenza-260.jpg", Stock = 10}
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
