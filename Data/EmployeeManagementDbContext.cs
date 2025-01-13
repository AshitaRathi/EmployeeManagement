using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection.Emit;

namespace EmployeeManagement.Data
{
    public class EmployeeManagementDbContext : IdentityDbContext
    {
        public EmployeeManagementDbContext(DbContextOptions<EmployeeManagementDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>().HasOne(e => e.Team).WithMany(e => e.Employees).HasForeignKey(e => e.TeamId);
            builder.Entity<Employee>().HasOne(j => j.Job).WithMany(e => e.Employees).HasForeignKey(e => e.JobId);

            base.OnModelCreating(builder);
        }

       
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
