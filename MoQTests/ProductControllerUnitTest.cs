using DemoAPI.Controllers;
using DemoAPI.Interface;
using DemoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MoQTests
{
    
    public class ProductControllerUnitTest
    {
        [Fact]
        public async void Test_Get_Product()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.GetProductByID(It.IsAny<int>())).Returns(Task.FromResult<Product>(new Product() { ID=1, Name="Green Tea" }));
            ProductController controller = new ProductController(mock.Object);
            var result = await controller.GetProduct(1);
            var actionResult = Assert.IsType<ActionResult<Product>>(result);
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var product = Assert.IsType<Product>(objectResult.Value);
            Assert.Equal("Green Tea", product.Name);
        }

        [Fact]
        public async void Test_Get_Products()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.GetProducts()).Returns(Task.FromResult<List<Product>>(new List<Product>() { new Product() { ID = 1, Name = "Green Tea" } }));
            ProductController controller = new ProductController(mock.Object);
            var result = await controller.GetAllProducts();
            var actionResult = Assert.IsType<ActionResult<List<Product>>>(result);
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var product = Assert.IsType<List<Product>>(objectResult.Value);
            Assert.Equal("Green Tea", product[0].Name);
        }

        [Fact]
        public async void Test_Get_CreateProduct()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.CreateProduct(It.IsAny<Product>())).Returns(Task.FromResult<Result>(new Result() { IsSuccess = true, Message="Done" }));
            ProductController controller = new ProductController(mock.Object);
            var result = await controller.CreateProduct(new Product());
            var actionResult = Assert.IsType<ActionResult<Result>>(result);
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnResult = Assert.IsType<Result>(objectResult.Value);
            Assert.True(returnResult.IsSuccess);
        }

        [Fact]
        public async void Test_Get_DeleteProduct()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.DeleteProduct(It.IsAny<int>())).Returns(Task.FromResult<Result>(new Result() { IsSuccess = true, Message = "Done" }));
            ProductController controller = new ProductController(mock.Object);
            var result = await controller.DeleteProduct(1);
            var actionResult = Assert.IsType<ActionResult<Result>>(result);
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnResult = Assert.IsType<Result>(objectResult.Value);
            Assert.True(returnResult.IsSuccess);
        }


    }
}
