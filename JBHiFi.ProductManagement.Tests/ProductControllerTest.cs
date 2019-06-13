using AutoMapper;
using CSharpFunctionalExtensions;
using FluentAssertions;
using JBHiFi.ProductManagement.API.Controllers;
using JBHiFi.ProductManagement.API.Model;
using JBHiFi.ProductManagement.Business.Entities;
using JBHiFi.ProductManagement.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections;
using System.Collections.Generic;
using Should.Core.Assertions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JBHiFi.ProductManagement.Tests
{
    [TestClass]
    public class ProductControllerTest
    {
        Mock<IQueryHandler> mockIQueryHandler;
        Mock<ICommandHandler> mockICommandHandler;
        MapperConfiguration configuration;

        [TestMethod]
        public void Shouold_Not_Be_Null_When_Product_Is_Requested()
        {
           //set up
            MockDependencies();
            var product = new Product { Brand = "Sony", Description = "smart TV", Id = "1", Model = "tv1" };
            mockIQueryHandler.Setup(x => x.Handle<string, Result<Product>>(It.IsAny<string>())).Returns(() => Result.Ok(product));
            mockICommandHandler.Setup(x => x.Handle<string>(It.IsAny<string>()));
          
            //arrange
            var productController = new ProductsController(mockIQueryHandler.Object, mockICommandHandler.Object, configuration.CreateMapper());
            var actualResult = productController.GetProduct("1") ;

            //test
            actualResult.Should().NotBeNull();
        }

        private  void MockDependencies()
        {
            mockIQueryHandler = new Mock<IQueryHandler>();
            mockICommandHandler = new Mock<ICommandHandler>();

             configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<Product, ProductForCreationDto>();
                cfg.CreateMap<Product, ProductForUpdateDto>();
            });

        }

        [TestMethod]
        public void Should_Return_Products_When_Searched_ByFilter()
        {
            //set up
            MockDependencies();
            IEnumerable<Product> products = new List<Product> 
                {
                new Product() {Brand = "Sony", Description = "smart TV", Id = "1", Model = "tv1" },
                new Product() {Brand = "Samsung", Description = "fridge", Id = "2", Model = "fr1" },
                new Product() {Brand = "LG", Description = "washing machine", Id = "3", Model = "ws1" },
                new Product() {Brand = "Whirpool", Description = "vaccum cleaner", Id = "4", Model = "vc1" },
                new Product() {Brand = "OKia", Description = "dishwasher", Id = "5", Model = "dis1" },

            };

            mockIQueryHandler.Setup(x => x.Handle<ProductFilterDto, Result<IEnumerable<Product>>>(It.IsAny<ProductFilterDto>())).Returns(() => Result.Ok(products));
            mockICommandHandler.Setup(x => x.Handle<string>(string.Empty));

            //arrange
            var productController = new ProductsController(mockIQueryHandler.Object, mockICommandHandler.Object,configuration.CreateMapper());
            var productFilter = new ProductFilterDto
            { Brand="Sony" , Description="", Model="" };

            //test
            var actualResult = productController.Get(productFilter) as OkObjectResult;
            var productsReturned = actualResult.Value as IEnumerable<ProductDto>;
            productsReturned.Should().HaveCount(5, "Five products match search criteria");
           
           

        }

        [TestMethod]
        public void Should_Not_Update_Product_With_Same_Description_And_Brand()
        {
            //set up
            MockDependencies();
            var product = new Product { Brand = "Sony", Description = "smart TV", Id = "1", Model = "tv1" };
            mockIQueryHandler.Setup(x => x.Handle<string, Result<Product>>(It.IsAny<string>())).Returns(() => Result.Ok(product));
            mockICommandHandler.Setup(x => x.Handle<ProductForUpdateDto>(It.IsAny<ProductForUpdateDto>()));

            //arrange
            var productToUpdate = new ProductForUpdateDto
            { Brand = "Sony", Description = "Sony", Model = "" , Id="1"};
            var productController = new ProductsController(mockIQueryHandler.Object, mockICommandHandler.Object,configuration.CreateMapper());
            var result = productController.UpdateProduct("1", productToUpdate) as BadRequestObjectResult;

            //test
            result.StatusCode.Value.Should().Be(400, "Brand and description are same");
        }

        [TestMethod]
        public void Should_Be_Able_To_Add_Product()
        {
            //set up
            MockDependencies();
            var product = new Product { Brand = "Sony", Description = "smart TV", Id = "1", Model = "tv1" };
            mockIQueryHandler.Setup(x => x.Handle<string, Result<Product>>(It.IsAny<string>())).Returns(() => Result.Ok(product));
            mockICommandHandler.Setup(x => x.Handle<ProductForUpdateDto>(It.IsAny<ProductForUpdateDto>()));

            //arrange
            var productToInsert = new ProductForCreationDto
            { Brand = "Sony", Description = "Sony", Model = "", Id = "1" };
            var productController = new ProductsController(mockIQueryHandler.Object, mockICommandHandler.Object, configuration.CreateMapper());
            var result = productController.AddProduct(productToInsert) as BadRequestObjectResult;

            //test
            result.StatusCode.Value.Should().Be(200, "Brand and description are same");
        }
    }
}
