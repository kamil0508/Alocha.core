using Alocha.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Alocha.Domain
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Sow> Sow { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Sow has a primary key 
            builder.Entity<Sow>().HasKey(s => s.SowId);

            //Sow has many SmallPigs 1:N relation
            builder.Entity<Sow>().HasMany(s => s.SmallPigs).WithOne(s => s.Sow).HasForeignKey(s => s.SowId);

            //SmallPig has a primary key
            builder.Entity<SmallPig>().HasKey(s => s.SmalPigsId);

            //User has many Sow 1:n relation
            builder.Entity<User>().HasMany(u => u.Sows).WithOne(s => s.User).HasForeignKey(s => s.UserId);

            // Determine restrict delete behaviour for each entity to avoid cycles cascade error
            builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                .ToList().ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
        }
    }
}
