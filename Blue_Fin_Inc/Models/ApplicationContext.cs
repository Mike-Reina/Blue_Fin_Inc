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
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;DataBase=BlueFinDB;Trusted_Connection=False;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(connectionString);
        }

        //We need the Dbset property to maniputlate the entries in the models
        public DbSet<Livestock> Livestocks { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<CartLivestock> CartLivestocks { get; set; }
        public DbSet<CartEquipment> CartEquipments { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
