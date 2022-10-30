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
        public DbSet<TradeOrder> tradeOrders { get; set; }
        public DbSet<TradingPair> tradingPairs { set; get; }
        public DbSet<RunningOrder> runningOrders { set; get; }
        public DbSet<BuyCondition> buyConditions { set; get; }
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
            modelBuilder.Entity<TradeOrder>(entity=>entity.HasKey(e=>e.Id));
            modelBuilder.Entity<TradingPair>(entity => entity.HasKey(e => e.Id));
            modelBuilder.Entity<RunningOrder>(entity => entity.HasKey(e => e.Id));
            modelBuilder.Entity<MarketPrint>().Property(b => b.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<BuyCondition>(entity => entity.HasKey(e => e.Id));
            modelBuilder.Entity<BuyCondition>(entity => entity.HasOne(e => e.tradingPair));
        }
      }
}
