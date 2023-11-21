using Azure.Data.Tables;
using DotNetDemo.Models;

namespace DotNetDemo.Api.Models
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Departments.Any()) return;

            var department1 = new Department
            {
                DepartmentId = 1,
                DepartmentName = "Admin"
            };

            // Department
            context.Add(
                department1
            );
            context.Add(
                new Department {
                    DepartmentId = 2,
                    DepartmentName = "HR"
                }
            );
            context.Add(
                new Department {
                    DepartmentId = 3,
                    DepartmentName = "Payroll"
                }
            );

            // Employee
            context.Add(
                new Employee
                {
                    EmployeeId = 1,
                    EmployeeName = "John",
                    DateOfBirth = new DateTime(1989, 01, 01),
                    Gender = Gender.Male,
                    Department = department1
                }
            );

            context.Add(
                new Employee
                {
                    EmployeeId = 2,
                    EmployeeName = "Matt",
                    DateOfBirth = new DateTime(1989, 01, 01),
                    Gender = Gender.Male,
                    DepartmentId = 2

                });

            context.Add(
                new Employee
                {
                    EmployeeId = 3,
                    EmployeeName = "Carol",
                    DateOfBirth = new DateTime(1989, 01, 01),
                    Gender = Gender.Female,
                    DepartmentId = 3

                });

            context.Add(
                new Employee
                {
                    EmployeeId = 4,
                    EmployeeName = "Tony",
                    DateOfBirth = new DateTime(1989, 01, 01),
                    Gender = Gender.Male,
                    DepartmentId = 3

                });

            context.SaveChanges();
        }

        public static void Initialize(AppSecondDbContext context)
        {
            if (context.Articles.Any()) return;

            Guid id = Guid.NewGuid();

            Article article = new Article
            {
                ArticleId = id,
                Subject = "test",
                Body = "test",
                CreationDate = DateTime.UtcNow,
                ModificationDate = DateTime.UtcNow,
                ArticleComments = new[] { new ArticleComment {
                    ArticleCommentId = Guid.NewGuid(),
                    Subject = "test",
                    Body = "test"
                } }
            };

            context.Articles.Add(article);

            context.SaveChanges();
        }

        public static async void InitializeAsync(InMemoryTableServiceClient tableServiceClient)
        {
            InMemoryTableClient client = new InMemoryTableClient();

            await client.AddEntityAsync(new Azure.ArticleComment{
                ArticleCommentId = Guid.NewGuid(),
                ArticleId = Guid.NewGuid(),
                Body = "test",
                CreationDate = DateTime.UtcNow,
                ModificationDate = DateTime.UtcNow,
                PartitionKey = "ArticleComment",
                RowKey = Guid.NewGuid().ToString(),
                Subject = "Test",
                IsApproved = false,
                IsDeclined = false
            });

            tableServiceClient.TableClients["ArticleComment"] = client;
        }
    }
}