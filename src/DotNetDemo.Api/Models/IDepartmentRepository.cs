using DotNetDemo.Models;

namespace DotNetDemo.Api.Models
{
    public interface IDepartmentRepository
    {
        Task<Department?> GetDepartment(int departmentId);

        Task<IEnumerable<Department>> GetDepartments();

        Task<Department?> AddDepartment(Department department);

        Task DeleteDepartment(int departmentId);

        Task<Department?> UpdateDepartment(Department department);
    }
}