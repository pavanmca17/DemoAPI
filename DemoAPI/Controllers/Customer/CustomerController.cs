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
    public class CustomerController : ControllerBase
    {
        private ICustomterRepository _customerRepository;

        public CustomerController(ICustomterRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        [HttpGet]
        [Route("api/customer/{hasorders}")]
        public async Task<ActionResult<List<Employee>>> GetCustomer(bool hasOrders)
        {
            Customer customer = await _customerRepository.GetCustomerwithOrders(hasOrders);
            //if(hasOrders)
            //{
            //    var orders = customer.orders;
            //}            
            return Ok(customer);
        }
        
    }
}
