using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Interface;
using DemoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DemoAPI.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    public class EmployeeControllerV2 : ControllerBase
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeControllerV2(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        [HttpGet]
        [Route("api/employees")]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            List<Employee> employees = await _employeeRepository.GetEmployees();
            return Ok(employees);
        }

        [HttpGet]
        [Route("api/employee/{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeByID(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByID(id);
            return Ok(employee);
        }

        [HttpPost]
        [Route("api/createemployee")]
        public async Task<ActionResult<Result>> CreateEmployee(Employee employee)
        {
            var result = await _employeeRepository.CreateEmployee(employee);
            return Ok(result);
        }

        [HttpDelete]
        [Route("api/deleteemployee/{id}")]
        public async Task<ActionResult<Result>> DeleteEmployee(int id)
        {
            var result = await _employeeRepository.DeleteEmployee(id);
            return Ok(result);
        }

        // DELETE: api/ApiWithActions/5
        [HttpPatch]
        [Route("api/updateemployee/{id}")]
        public async Task<ActionResult<Result>> UpdateEmployee(int id, String Name)
        {
            var result = await _employeeRepository.UpdateEmployee(id, Name);
            return Ok(result);
        }
    }
}
