using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImagePost.Data
{
    public class ImageContext : DbContext
    {
        private string _conStr;
        public ImageContext(string conStr)
        {
            _conStr = conStr;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conStr);
        }

        public DbSet<Image> Images { get; set; }
    }
}
