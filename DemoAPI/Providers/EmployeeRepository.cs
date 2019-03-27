using DemoAPI.Interface;
using DemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace DemoAPI
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _employees;

        public  EmployeeRepository(IConfiguration config, IOptions<Settings> settings,ILogger<EmployeeRepository> logger)
        {

            var client = new MongoClient(settings.Value.MongoDBConnectionString);
            var database = client.GetDatabase(settings.Value.EmployeeDatabase);
            _employees = database.GetCollection<Employee>("Employees");
        }
        public async Task<Result> CreateEmployee(Employee employee)
        {
            await _employees.InsertOneAsync(employee);
            return await Task.FromResult<Result>(new Result() { IsSuccess = true,Message= "Message" }); 
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _employees.Find<Employee>(emp => true).ToListAsync();
        }

        public async Task<Employee> GetEmployeeByID(int EmpId)
        {
            return await _employees.Find<Employee>(emp => emp.EmpID == EmpId).FirstOrDefaultAsync();
        }


        public async Task<Result> DeleteEmployee(int EmpId)
        {
            await _employees.DeleteOneAsync<Employee>(emp => emp.EmpID == EmpId);
            return await Task.FromResult<Result>(new Result() { IsSuccess = true, Message = "Message" });
        }

       

        public async Task<Result> UpdateEmployee(int id, String Name)
        {
            //await _employees.UpdateOne<Employee>(emp => emp.EmpID == id, employee);
            var filter = Builders<Employee>.Filter.Eq("EmpID", id);
            var update = Builders<Employee>.Update.Set("Name", Name);
            await _employees.UpdateOneAsync(filter, update);
            return await Task.FromResult<Result>(new Result() { IsSuccess = true, Message = "Message" });
        }
    }
}
