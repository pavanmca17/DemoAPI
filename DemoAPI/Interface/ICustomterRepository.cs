using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Interface
{
    public interface ICustomterRepository
    {
        Task<Customer> GetCustomerwithOrders(bool hasOrders);
        
    }
}
