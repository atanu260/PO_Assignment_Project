﻿using Microsoft.EntityFrameworkCore;
using PO_Assignment_Project.Models;

namespace PO_Assignment_Project.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
        {
        }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetails> PurchaseOrderDetails { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Site> Site { get; set; }

    }
}
