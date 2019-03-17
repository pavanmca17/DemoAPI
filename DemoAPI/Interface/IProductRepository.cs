using DemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Interface
{
    public interface IProductRepository
    {
       Task<List<Product>> GetProducts();

       Task<Product> GetProductByID(int id);

       Task<Result> CreateProduct(Product product);

       Task<Result> DeleteProduct(int id);

       Task<Result> UpdateProduct(int id,String Name);


    }
}
