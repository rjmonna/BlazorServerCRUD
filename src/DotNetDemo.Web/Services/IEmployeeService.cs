using DotNetDemo.Models;

namespace DotNetDemo.Web.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();

        Task<Employee> GetEmployee(int id);

        Task DeleteEmployee(int id);

        Task AddEmployee(Employee employee);

        Task UpdateEmployee(Employee employee);
    }
}