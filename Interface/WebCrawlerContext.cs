using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interface
{
    public class WebCrawlerContext : DbContext
    {
        public DbSet<GameNews> GameNews { get; set; }

        public void StartConnection()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            
            options.UseSqlServer("Data Source = DESKTOP-O9CABQT\\SQLEXPRESS; Initial Catalog = WebCrawlerDB; Integrated Security = True");
        }
    }

}
