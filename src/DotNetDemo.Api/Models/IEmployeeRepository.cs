using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetDemo.Models;

namespace DotNetDemo.Api.Models
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();

        Task<Employee?> GetEmployee(int employeeId);

        Task<Employee?> AddEmployee(Employee employee);

        Task<Employee?> UpdateEmployee(Employee employee);

        Task DeleteEmployee(int employeeId);
    }
}