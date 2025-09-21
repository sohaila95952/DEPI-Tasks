
using api1.Data;
using api1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIEmployee2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            var emp = await _employeeServices.AddEmployee(employee);
            if (emp != null)
                return Ok(emp);
            else
                return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeServices.GetEmployees();
            if (employees != null)
                return Ok(employees);
            else
                return NotFound();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeByID(int id)
        {
            var employee = await _employeeServices.GetEmployeeById(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var emp = await _employeeServices.DeleteEmployee(id);
            if (emp != null)
                return Ok();
            else
                return NotFound();
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            var emp = await _employeeServices.UpdateEmployee(id, employee);
            if (emp != null)
                return Ok(emp);
            else
                return NotFound();

        }
    }
}