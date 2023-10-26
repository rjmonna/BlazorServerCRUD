using System.Reflection;
using BlazorServerCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerCRUD.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Department>()
                .HasKey(e => e.DepartmentID);

            modelBuilder
                .Entity<Department>()
                .Property(p => p.DepartmentID)
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<Employee>()
                .HasKey(e => e.EmployeeID);

            modelBuilder
                .Entity<Employee>()
                .Property(p => p.EmployeeID)
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<Department>()
                .HasMany(e => e.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId);

            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // // Department
            // modelBuilder.Entity<Department>().HasData(
            //     new Department {
            //         DepartmentID = 1,
            //         DepartmentName = "Admin"
            //     }
            // );
            // modelBuilder.Entity<Department>().HasData(
            //     new Department {
            //         DepartmentID = 2,
            //         DepartmentName = "HR"
            //     }
            // );
            // modelBuilder.Entity<Department>().HasData(
            //     new Department {
            //         DepartmentID = 3,
            //         DepartmentName = "Payroll"
            //     }
            // );

            // // Employee
            // modelBuilder.Entity<Employee>().HasData(
            //     new Employee
            //     {
            //         EmployeeID = 1,
            //         EmployeeName = "John",
            //         DateOfBirth = new DateTime(1989, 01, 01),
            //         Gender = Gender.Male,
            //         DepartmentId = 1
            //     }
            // );
        }
    }
}