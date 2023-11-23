using DotNetDemo.Infrastructure;
using DotNetDemo.Infrastructure.Contracts;
using DotNetDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            return Ok(await _employeeRepository.GetEmployees());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployee(int id)
        {
            return Ok(await _employeeRepository.GetEmployee(id));
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee(Employee employee)
        {
            return Ok(await _employeeRepository.AddEmployee(employee));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEmployee(Employee employee)
        {
            return Ok(await _employeeRepository.UpdateEmployee(employee));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            await _employeeRepository.DeleteEmployee(id);

            return Ok();
        }
    }
}
