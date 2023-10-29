using BlazorServerCRUD.Models;

namespace BlazorServerCRUD.Api.Models
{
    public interface IDepartmentRempository
    {
        Task<Department?> GetDepartment(int departmentId);

        Task<IEnumerable<Department>> GetDepartments();
    }
}