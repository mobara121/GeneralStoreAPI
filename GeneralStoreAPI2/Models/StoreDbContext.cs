using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI2.Models
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext() : base("DefaultConnection") { }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
    }
}