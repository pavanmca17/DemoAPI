using DemoAPI.Interface;
using DemoAPI.Models;
using EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Impl
{
    public class ProductRepository : IProductRepository
    {
        private ProductContext _productContext;

        public ProductRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = _productContext.products.ToList();
            if(products !=null)
            {
                return await Task.FromResult<List<Product>>(products.ToList());
            }
            else
            {
                return await Task.FromResult<List<Product>>(new List<Product>());
            }          

        }

        public async Task<Product> GetProductByID(int id)
        {
            return await _productContext.products.FindAsync(id);
        }

        public async Task<Result> CreateProduct(Product product)
        {
            await _productContext.AddAsync<Product>(product);
            await _productContext.SaveChangesAsync();           
            return await Task.FromResult<Result>(new Result() { IsSuccess = true, Message = "Done" });

        }

        public async Task<Result> DeleteProduct(int id)
        {
            var product = await _productContext.FindAsync<Product>(id);
            if(product != null)
            {
                _productContext.Remove<Product>(product);
                await _productContext.SaveChangesAsync();

            }
            else
            {

            }
           
            return await Task.FromResult<Result>(new Result() { IsSuccess = true, Message = "Done" });
        }

        public async Task<Result> UpdateProduct(int id, String Name)
        {
            var product = await _productContext.products.FindAsync(id);
            product.Name = Name;
            _productContext.products.Update(product);
            await _productContext.SaveChangesAsync();
            return await Task.FromResult<Result>(new Result() { IsSuccess = true, Message = "Done" });
        }

    }
  
}
