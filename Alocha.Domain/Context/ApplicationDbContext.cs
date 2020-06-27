using Alocha.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Alocha.Domain
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Sow has a primary key 
            builder.Entity<Sow>().HasKey(s => s.SowId);

            //Sow has many SmallPigs 1:N relation
            builder.Entity<Sow>().HasMany(s => s.SmallPigs).WithOne(s => s.Sow).HasForeignKey(s => s.SowId);

            //SmallPig has a primary key
            builder.Entity<SmallPig>().HasKey(s => s.SmalPigsId);

            //User has one Customer 1:1 relation
            builder.Entity<User>().HasOne(u => u.Customer).WithOne(c => c.User).HasForeignKey<Customer>(u => u.UserId);

            builder.Entity<Customer>().HasMany(c => c.Sows).WithOne(s => s.Customer).HasForeignKey(c => c.CustomerId);
        }
    }
}
