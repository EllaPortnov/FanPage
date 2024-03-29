﻿using Microsoft.EntityFrameworkCore;


namespace InternetFanPage.Models
{
    public class FanPageContext : DbContext
    {
        public FanPageContext()
        {
        }

        public FanPageContext(DbContextOptions<FanPageContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Filename=./PageDB.db");
            }
        }
    }
}