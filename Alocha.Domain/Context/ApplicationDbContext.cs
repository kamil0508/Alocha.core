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

            ////Sow has a primary key 
            //builder.Entity<Sow>().HasKey(s => s.SowId);

            ////Sow has many SmallPigs 1:N relation
            //builder.Entity<Sow>().HasMany(s => s.SmallPigs).WithOne(s => s.Sow).HasForeignKey(s => s.SowId);

            ////SmallPig has a primary key
            //builder.Entity<SmallPig>().HasKey(s => s.SmalPigsId);

            ////User has many Sows 1:N relation
            //builder.Entity<User>().HasMany(u => u.Sows).WithOne(s => s.User).HasForeignKey(u => u.UserId);
        }
    }
}
