namespace AzureMVCDashDocsSample.Migrations
{
    using AzureMVCDashDocsSample.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AzureMVCDashDocsSample.Services.DashDocsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AzureMVCDashDocsSample.Services.DashDocsContext context)
        {
            {
                Guid customerId = Guid.NewGuid();
                Guid userIdKron = Guid.NewGuid();
                Guid userIdTron = Guid.NewGuid();

                context.Customers.AddOrUpdate(
                c => c.Id,
                new Customer { Id = customerId, Name = "DashDocDevs" }
                );
                context.Users.AddOrUpdate(
                u => u.Id,
                new User
                {
                    Id = userIdKron,
                    FirstName = "Kron",
                    LastName =
                "Linda",
                    CustomerId = customerId
                },
                new User
                {
                    Id = userIdTron,
                    FirstName = "Tron",
                    LastName =
                "Spagner",
                    CustomerId = customerId
                }
                );
            }
        }



    }
}