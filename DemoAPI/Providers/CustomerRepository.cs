using DemoAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Providers
{
    public class CustomerRepository : ICustomterRepository
    {
        public async Task<Customer> GetCustomerwithOrders(bool hasOrders)
        {
            return await Task.FromResult<Customer>(new Customer(10,"Pavan"));
        }
    }
}
