using EmployeeManager.Data.Seed;
using EmployeeManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; } = default!;
    public DbSet<Address> Addresses { get; set; } = default!;
    public DbSet<Country> Countries { get; set; } = default!;
    public DbSet<Salary> Salaries { get; set; } = default!;
    public DbSet<JobCategory> JobCategories { get; set; } = default!;
    public DbSet<EmployeeJobCategory> EmployeeJobCategories { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Superior)
            .WithMany(e => e.Subordinates)
            .HasForeignKey(e => e.SuperiorId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Salary>()
            .HasOne(s => s.Employee)
            .WithMany(e => e.Salaries)
            .HasForeignKey(s => s.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EmployeeJobCategory>()
            .HasKey(ejc => new { ejc.EmployeeId, ejc.JobCategoryId });

        modelBuilder.Entity<EmployeeJobCategory>()
            .HasOne(ejc => ejc.Employee)
            .WithMany(e =>  e.EmployeeJobCategories)
            .HasForeignKey(ejc => ejc.EmployeeId);

        modelBuilder.Entity<EmployeeJobCategory>()
            .HasOne(ejc => ejc.JobCategory)
            .WithMany(jc => jc.EmployeeJobCategories)
            .HasForeignKey(ejc => ejc.JobCategoryId);

        modelBuilder.SeedCountries();
        modelBuilder.SeedJobCategories();

        base.OnModelCreating(modelBuilder);
    }
}
