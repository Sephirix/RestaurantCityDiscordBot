using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace RestaurantCityDiscordBot.Resources.Database
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<Trade> Trades { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder Options)
        {
            string DbLocation = Environment.CurrentDirectory;
            Options.UseSqlite($@"Data Source={DbLocation}\Data\Database.sqlite");
        }
    }
}

