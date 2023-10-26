using BlazorServerCRUD.Models;

namespace BlazorServerCRUD.Api.Models
{
    public class DepartmentRepository : IDepartmentRempository
    {
        private readonly AppDbContext _appDbContext;

        public DepartmentRepository(AppDbContext appDbContext) =>
            _appDbContext = appDbContext;

        public Department GetDepartment(int departmentId)
        {
            return _appDbContext.Departments.FirstOrDefault(e => e.DepartmentId  == departmentId);
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _appDbContext.Departments.ToList();
        }
    }
}