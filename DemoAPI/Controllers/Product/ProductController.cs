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
    
    [ApiVersion("1.0")]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;


        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet]
        [Route("api/products/allproducts")]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            List<Product> products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet]
        [Route("api/products/product/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductByID(id);
            return Ok(product);
        }

        [HttpPost]
        [Route("api/products/createproduct")]
        public async Task<ActionResult<Result>> CreateProduct(Product product)
        {
            var result = await _productRepository.CreateProduct(product);
            return Ok(result);
        }

        [HttpDelete]
        [Route("api/products/deleteproduct/{id}")]
        public async Task<ActionResult<Result>> DeleteProduct(int id)
        {
            var result = await _productRepository.DeleteProduct(id);
            return Ok(result);
        }

        [HttpPatch]
        [Route("api/products/updateproduct/{id}")]
        public async Task<ActionResult<Result>> UpdateProduct(int id,String Name)
        {
            var result = await _productRepository.UpdateProduct(id, Name);
            return Ok(result);
        }
    }
}
