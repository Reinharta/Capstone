﻿using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Capstone.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string UserRole { get; set; }

        [Display(Name="Organization Name")]
        public string OrganizationName { get; set; }

        [Display(Name = "First Name")]
        public string SupporterFirstName { get; set; }

        [Display(Name="Last Name")]
        public string SupporterLastName { get; set; }



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<NonprofitOrganization> NonprofitOrganizations { get; set; }
        public DbSet<Supporter> Supporters { get; set; }
        public DbSet<DonationItem> DonationItem { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<OrganizationCategory> OrganizationCategories { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<ImageUpload> ImageUploads { get; set; }
        public DbSet<DonationBasket> DonationBaskets { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
    }
}