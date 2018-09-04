namespace Capstone.Migrations
{
    using Capstone.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Capstone.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Capstone.Models.ApplicationDbContext context)
        {
            ////ADDING ADMIN HERE//
            if (!(context.Users.Any(u => u.UserName == "admin@email.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser { UserName = "admin@email.com", PhoneNumber = "12345678911", Email = "admin@email.com", UserRole = "Admin" };
                userManager.Create(userToInsert, "Password1!");
                userManager.AddToRole(userToInsert.Id, userToInsert.UserRole);
            }


            ///SEEDING ORG CATEGORIES
            context.OrganizationCategories.AddOrUpdate(x => x.Name,
                new OrganizationCategory() { Name = "Shelter" },
                new OrganizationCategory() { Name = "Food Pantry" },
                new OrganizationCategory() { Name = "Public School" },
                new OrganizationCategory() { Name = "Animal Shelter" },
                new OrganizationCategory() { Name = "Other"}
                );

            context.ItemCategories.AddOrUpdate(x => x.Name,
                new ItemCategory() { Name = "Adult Clothing"},
                new ItemCategory() { Name = "Craft & Office Supplies"},
                new ItemCategory() { Name = "Personal Hygiene"},
                new ItemCategory() { Name = "Children's & Infant Clothing" },
                new ItemCategory() { Name = "Food - Non-Perishables"},
                new ItemCategory() { Name = "Food - Perishables"},
                new ItemCategory() { Name = "Electronics"},
                new ItemCategory() { Name = "Media"},
                new ItemCategory() { Name = "Toys & Games"},
                new ItemCategory() { Name = "Other"}
                );

            //context.CartItem.AddOrUpdate(x => x.BasketId,
            //    new CartItem()
            //    {
            //        SupporterId = 4,
            //        DateCreated = DateTime.Today,
            //        ProductId = 9,
            //        Quantity = 2
            //    },
            //    new CartItem()
            //    {
            //        SupporterId = 4,
            //        DateCreated = DateTime.Today,
            //        ProductId = 10,
            //        Quantity = 12
            //    }             
            //);

            //context.DonationBaskets.AddOrUpdate(x => x.BasketId,
            //    new DonationBasket()
            //    {
            //        SupporterId = 4,
            //        OrganizationId = 65,
            //        DateCreated = DateTime.Today,
            //        BasketPending = false,
            //        Received = false,
                    
            //    }
            //);





            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
