
using System.Reflection;
using Demo.DAL.Models.DepartmentModule;
using Demo.DAL.Models.EmployeeModule;
using Demo.DAL.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Demo.DAL.Data.Contexts;

// ApplicationDbContext inherit from IdentityDbContext inherit from DbContext => [undirect]

// Dependency Injection
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // DI
    // {
    //     optionsBuilder.UseSqlServer("Server=.;Database=Demo;Trusted_Connection=True;TrustServerCertificate=True;");
    //
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigrations());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<IdentityUser>().ToTable("Users");
    }
    
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    
    
}