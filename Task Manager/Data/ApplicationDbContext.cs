using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Task_Manager.Models;

namespace Task_Manager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Duty>().HasOne(d => d.Status).WithMany(s => s.Duties).HasForeignKey(d => d.StatusId);
            modelBuilder.Entity<AppUser>().HasOne(u => u.AppRole).WithMany(r => r.AppUsers).HasForeignKey(u => u.AppRoleId);        
        }
        public DbSet<Duty> Duties { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<AppRole> AppRoles { get; set; }

    }
}
