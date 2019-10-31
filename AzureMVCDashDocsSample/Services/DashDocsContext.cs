using AzureMVCDashDocsSample.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AzureMVCDashDocsSample.Services
{
    public class DashDocsContext : DbContext
    {
        public DashDocsContext()
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}