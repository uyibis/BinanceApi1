using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinanceApi1
{
    class DataContext : DbContext
    {
        /*public DataContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }*/
        public DbSet<MarketPrint> marketPrints { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"server=localhost;port=3306;userid=root;password=;database=cryptodata;persistsecurityinfo=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MarketPrint>(entity =>
            {
                entity.HasKey(e => e.Id);

            });
            modelBuilder.Entity<MarketPrint>().Property(b => b.Id).ValueGeneratedOnAdd();
        }
      }
}
