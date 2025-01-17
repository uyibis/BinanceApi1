﻿// <auto-generated />
using System;
using BinanceApi1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BinanceApi1.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221026014836_mig6")]
    partial class mig6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BinanceApi1.BuyCondition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("maxInvestment")
                        .HasColumnType("float");

                    b.Property<float>("percentBalance")
                        .HasColumnType("float");

                    b.Property<int>("tradingPairId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("tradingPairId");

                    b.ToTable("buyConditions");
                });

            modelBuilder.Entity("BinanceApi1.MarketPrint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("max")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("min")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("pair")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("pricePercentPosition")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("time")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("marketPrints");
                });

            modelBuilder.Entity("BinanceApi1.RunningOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("initialRate")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("initialWorth")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("pair")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("runningOrders");
                });

            modelBuilder.Entity("BinanceApi1.TradeOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BinanceOrderId")
                        .HasColumnType("int");

                    b.Property<string>("ClientOrderId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("pair")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("quantity")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("tradeType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("tradeOrders");
                });

            modelBuilder.Entity("BinanceApi1.TradingPair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("tradingPairs");
                });

            modelBuilder.Entity("BinanceApi1.BuyCondition", b =>
                {
                    b.HasOne("BinanceApi1.TradingPair", "tradingPair")
                        .WithMany()
                        .HasForeignKey("tradingPairId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
