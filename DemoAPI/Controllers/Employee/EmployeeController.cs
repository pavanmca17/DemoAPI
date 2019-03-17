using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    
    
    [ApiVersion("1.0")]
    public class EmployeeController : Controller
    {
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
    }
}