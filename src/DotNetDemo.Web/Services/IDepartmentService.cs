using DotNetDemo.Models;

namespace DotNetDemo.Web.Services
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