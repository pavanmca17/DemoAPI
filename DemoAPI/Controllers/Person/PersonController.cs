using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{


    [ApiVersion("1.0")]
    [ApiController]
    public class PersonController : Controller
    {
        public PersonController()
        {
            People = new List<Person>()
            {
                new Person() {Id=1, Name = "Fred Blogs", Email="fred.blogs@someemail.com" },
                new Person() {Id=2, Name = "James Smith", Email="james.smith@someemail.com" },
                new Person() {Id=3, Name = "Jerry Jones", Email="jerry.jones@someemail.com" }
            };
        }

        public List<Person> People { get; }

        [HttpGet]
        [Route("api/Persons")]
        public IEnumerable<Person> Get()
        {
            return People;
        }

        [HttpGet()]
        [Route("api/Persons/{id}")]
        public IActionResult Get(int id)
        {
            Person person = People.Where(p => p.Id == id).FirstOrDefault();
            if (person == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(person);
            }
        }



    }
}