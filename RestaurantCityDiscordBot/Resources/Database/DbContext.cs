using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RestaurantCityDiscordBot.Resources.Database
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<Trade> Trades { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            string DbLocation = Assembly.GetEntryAssembly().Location.Replace(@"bin\Debug\netcoreapp2.1", @"Data\Database.sqlite");
            Options.UseSqlite("Data Source = " + DbLocation);
        }
    }
}

