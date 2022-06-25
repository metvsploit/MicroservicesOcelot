using Microsoft.AspNetCore.Mvc.Testing;
using MobileStore.Domain.Entities;
using MobileStore.Product;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MobileStore.IntegrationTest.ProductTests
{
    public class CartControllerTest
    {
        private readonly HttpClient _httpClient;

        public CartControllerTest()
        {
            var webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            _httpClient = webHost.CreateClient();
        }

        [Fact]
        public async Task Get_NoContent_When_Cart_Is_Empty()
        {
            var response = await _httpClient.GetAsync("api/product/cart");

            Assert.Equal("NoContent", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Add_Product_To_Cart()
        {
            //Act
            var model = new Smartphone {Id=1, Name = "test", Brand = "test", Price = 1234 };
            int quantity = 2;
            string json = JsonConvert.SerializeObject(model);
            HttpContent content =  new StringContent(json, Encoding.UTF8, "application/json");
             var response =await _httpClient.PostAsync($"api/product/cart/{quantity}", content);

            var result = await _httpClient.GetAsync("api/product/cart");
            string jsonCartContent = await result.Content.ReadAsStringAsync();
            var cartLine = JsonConvert.DeserializeObject<List<CartItem>>(jsonCartContent);
            

            //Assert

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Single(cartLine);
            Assert.Equal(2, cartLine[0].Quantity);
        }

        [Fact]
        public async Task Get_ListCart_When_Cart_Is_NotEmpty()
        {
            var model = new Smartphone { Id = 1, Name = "test", Brand = "test", Price = 1234 };
            int quantity = 2;
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"api/product/cart/{quantity}", content);

            var response = await _httpClient.GetAsync("api/product/cart");
            string json = await response.Content.ReadAsStringAsync();
            var cartLine = JsonConvert.DeserializeObject<List<CartItem>>(json);

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.NotNull(cartLine);
        }
    }
}
