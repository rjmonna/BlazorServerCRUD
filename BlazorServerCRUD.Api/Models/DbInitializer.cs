using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorServerCRUD.Models;

namespace BlazorServerCRUD.Api.Models
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Departments.Any()) return;

            var department1 = new Department
            {
                DepartmentID = 1,
                DepartmentName = "Admin"
            };

            // Department
            context.Add(
                department1
            );
            context.Add(
                new Department {
                    DepartmentID = 2,
                    DepartmentName = "HR"
                }
            );
            context.Add(
                new Department {
                    DepartmentID = 3,
                    DepartmentName = "Payroll"
                }
            );

            // Employee
            context.Add(
                new Employee
                {
                    EmployeeID = 1,
                    EmployeeName = "John",
                    DateOfBirth = new DateTime(1989, 01, 01),
                    Gender = Gender.Male,
                    Department = department1
                }
            );

            context.SaveChanges();
        }
    }
}