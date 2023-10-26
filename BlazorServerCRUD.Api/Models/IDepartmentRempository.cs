using BlazorServerCRUD.Models;

namespace BlazorServerCRUD.Api.Models
{
    public interface IDepartmentRempository
    {
        Department GetDepartment(int departmentId);

        IEnumerable<Department> GetDepartments();
    }
}