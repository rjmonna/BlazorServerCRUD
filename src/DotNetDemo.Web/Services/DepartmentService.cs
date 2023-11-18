using DotNetDemo.Models;

namespace DotNetDemo.Web.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _httpClient.GetFromJsonAsync<Department[]>("api/department");
        }

        public async Task AddDepartment(Department department)
        {
            await _httpClient.PostAsJsonAsync($"api/department", department);
        }

        public async Task DeleteDepartment(int id)
        {
            await _httpClient.DeleteAsync($"api/department/{id}");
        }

        public async Task<Department> GetDepartment(int id)
        {
            return await _httpClient.GetFromJsonAsync<Department>($"api/department/{id}");
        }

        public async Task UpdateDepartment(Department department)
        {
            await _httpClient.PutAsJsonAsync($"api/department", department);
        }
    }
}