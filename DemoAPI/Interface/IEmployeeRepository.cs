using DemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Interface
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();

        Task<Employee> GetEmployeeByID(int EmpID);

        Task<Result> CreateEmployee(Employee employee);

        Task<Result> DeleteEmployee(int EmpID);

        Task<Result> UpdateEmployee(int EmpID, String Name);
    }
}
