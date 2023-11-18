using DotNetDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetDemo.Api.Models
{
    public interface IDepartmentRempository
    {
        Task<Department?> GetDepartment(int departmentId);

        Task<IEnumerable<Department>> GetDepartments();

        Task<Department?> AddDepartment(Department department);

        Task DeleteDepartment(int departmentId);

        Task<Department?> UpdateDepartment(Department department);
    }
}