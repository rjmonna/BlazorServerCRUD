using BlazorServerCRUD.Api.Models;
using BlazorServerCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorServerCRUD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRempository _departmentRepository;

        public DepartmentController(IDepartmentRempository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetDepartments()
        {
            return Ok(await _departmentRepository.GetDepartments());
        }
    }
}
