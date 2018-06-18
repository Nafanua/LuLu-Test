using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LULUTest.Models
{

    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string SecondName { get; set; }

        public string Adress { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {

            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<BookModel> Books { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookModel>().HasKey(i => i.Id);
            modelBuilder.Entity<BookModel>().ToTable("Books");
            modelBuilder.Entity<BookModel>().Property(i => i.Name).HasMaxLength(500).IsRequired();
            modelBuilder.Entity<BookModel>().Property(i => i.Author).HasMaxLength(200).IsRequired();
            modelBuilder.Entity<BookModel>().Property(i => i.Description).IsRequired();
            modelBuilder.Entity<BookModel>().Property(i => i.Price).IsRequired();
            modelBuilder.Entity<BookModel>().Property(i => i.PubDate).IsRequired();
            modelBuilder.Entity<BookModel>().Property(i => i.Quantity).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}