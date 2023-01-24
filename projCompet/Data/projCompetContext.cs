using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projCompet.Models;

namespace projCompet.Data
{
    public class projCompetContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CustomerRole> CustomerRoles { get; set; }
        public projCompetContext (DbContextOptions<projCompetContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerRole>()
                .HasKey(t => new { t.CustomerId, t.RoleId });

            modelBuilder.Entity<CustomerRole>()
                .HasOne(pt => pt.CustomerUser)
                .WithMany(p => p.Roles)
                .HasForeignKey(pt => pt.CustomerId);

            modelBuilder.Entity<CustomerRole>()
                .HasOne(pt => pt.Role)
                .WithMany(t => t.Customers)
                .HasForeignKey(pt => pt.RoleId);
        }
    }
}
