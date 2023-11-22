using DotNetDemo.Models;

namespace DotNetDemo.Services.Contracts
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartments();

        Task<Department> GetDepartment(int id);

        Task DeleteDepartment(int id);

        Task AddDepartment(Department department);

        Task UpdateDepartment(Department department);
    }
}