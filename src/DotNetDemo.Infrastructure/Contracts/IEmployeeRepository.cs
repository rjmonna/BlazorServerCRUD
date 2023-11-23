using DotNetDemo.Models;

namespace DotNetDemo.Infrastructure.Contracts
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