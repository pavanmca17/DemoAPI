using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoAPI.Controllers
{
    
    
    [ApiVersion("1.0")]
    [ApiController]
    public class EmployeeController : Controller
    {
        public EmployeeController(ILogger<EmployeeController> logger)
        {
            logger.LogInformation("Created");
        }

        [HttpGet]
        [Route("api/employees/employee/{id}")]
        public ActionResult<EmployeeData> GetByID(int id)
        {
            var employee = new EmployeeData()
            {
                ID = id,
                FirstName = "Pavan",
                LastName = "Kumar",
                DateOfBirth = DateTime.Now.AddYears(-30)
            };

            return Ok(employee);
        }

        [HttpGet]
        [Route("api/getallemployees")]
        public ActionResult<List<EmployeeData>> GetEmployees()
        {
            var employees = new List<EmployeeData>()
            {
                new EmployeeData()
                {
                    ID = 1,
                    FirstName = "Pavan",
                    LastName = "Kumar",
                    DateOfBirth = DateTime.Now.AddYears(-30)
                },
                new EmployeeData()
                {
                    ID = 2,
                    FirstName = "Satish",
                    LastName = "Kumar",
                    DateOfBirth = DateTime.Now.AddYears(-37)
                },
                new EmployeeData()
                {
                    ID = 3,
                    FirstName = "Anik",
                    LastName = "Joseph",
                    DateOfBirth = DateTime.Now.AddYears(-3)
                },
                 new EmployeeData()
                {
                    ID = 3,
                    FirstName = "Joel",
                    LastName = "Ashvik",
                    DateOfBirth = DateTime.Now.AddYears(-1)
                }
            };
            

            return Ok(employees);
        }
    }
}
