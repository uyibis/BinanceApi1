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
    [Migration("20220818024808_mig")]
    partial class mig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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
#pragma warning restore 612, 618
        }
    }
}
