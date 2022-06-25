using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using MobileStore.Domain.Entities;
using MobileStore.Product;
using MobileStore.Product.Controllers;
using MobileStore.Product.Domain.Response;
using MobileStore.Product.Domain.Service;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MobileStore.IntegrationTest
{
    public class ProductControllerTest
    {
        private readonly HttpClient _httpClient;

        public ProductControllerTest()
        {
            var webHost =  new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            _httpClient = webHost.CreateClient();
        }

        [Fact]
        public async Task GetAllProductsAsync()
        {
        
            //Act
            HttpResponseMessage response = await _httpClient.GetAsync("api/product");
            string json = await response.Content.ReadAsStringAsync();
            List<Smartphone> result = JsonConvert.DeserializeObject<List<Smartphone>>(json);
            //Assert
            Assert.Equal(2,result.Count);
        }

        [Fact]
        public async Task GetProductsById_ObjectIsExists_ReturnOkRequest_NotNull()
        {
          
            //Act
            HttpResponseMessage response = await _httpClient.GetAsync("api/product/2");
            string json = await response.Content.ReadAsStringAsync();
            Smartphone result = JsonConvert.DeserializeObject<Smartphone>(json);
            //Assert
            Assert.NotNull(result);
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task GetAllProductsById_ObjectIsNotExists_ReturnBadRequest()
        {
         
            //Act
            HttpResponseMessage response = await _httpClient.GetAsync($"api/product/100");

            //Assert
            Assert.Equal("BadRequest", response.StatusCode.ToString());
        }

        [Fact]
        public async Task CreateProductAsync_WhenModelStateIsValid()
        {
            //Arrange
            var model = new Smartphone { Name = "test", Brand = "test", Price = 1234 };
            var smartResponse = new SmartphoneResponse<Smartphone> {
                Data = model, Descripton= "Продукт успешно добавлен" };
            var mockService = new Mock<ISmartphoneService>();
            mockService.Setup(m => m.CreateSmartphone(model)).ReturnsAsync(smartResponse);
            var controller = new ProductController(mockService.Object);
            //Act
            var result =  await controller.Create(model);
            var okResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task CreateProductAsync_WhenModelStateIsNotValid()
        {
            //Arrange
            var model = new Smartphone { Name = "test", Price = 234 };
            var smartResponse = new SmartphoneResponse<Smartphone>
            {
                Data = model,
                Descripton = "Продукт успешно добавлен"
            };
            var mockService = new Mock<ISmartphoneService>();
            mockService.Setup(m => m.CreateSmartphone(model)).ReturnsAsync(smartResponse);
            var controller = new ProductController(mockService.Object);
            controller.ModelState.AddModelError("Brand", "Brand is required");
            //Act
            var result = await controller.Create(model);
            var badResult = result as BadRequestResult;
            //Assert
            Assert.Equal(400, badResult.StatusCode);
        }
    }
}
