using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Data
{
    public class DB : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<CustomerRole> CustomerRole { get; set; }
        public DbSet<Competition> Competition { get; set; }
        public DbSet<StatusCompetition> StatusCompetition { get; set; }
        public DbSet<TaskCompetition> TaskCompetition { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<CategoryTask> CategoryTask { get; set; }
        public DB(DbContextOptions<DB> options) : base(options) 
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
