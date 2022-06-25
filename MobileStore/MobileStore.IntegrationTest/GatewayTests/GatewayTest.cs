using Microsoft.AspNetCore.Mvc.Testing;
using MobileStore.Domain.Entities;
using MobileStore.Gateway;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MobileStore.IntegrationTest.GatewayTests
{
    public class GatewayTest
    {
        private readonly HttpClient _httpClient;

        public GatewayTest()
        {
            _httpClient = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { }).CreateClient();
        }

        [Fact]
        public async Task GetAllProducts_Return_Ok_NotNull()
        {
            var response = await _httpClient.GetAsync("gateway/product");

            string json = await response.Content.ReadAsStringAsync();
            List<Smartphone> result = JsonConvert.DeserializeObject<List<Smartphone>>(json);

            Assert.NotNull(result);
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Create_Product_By_Authorized_User()
        {
            //Arrange
            var model = new Smartphone { Name = "Lenovo B54", Brand = "Lenovo", Price = 1234 };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyMUBtYWlsLnJ1In0.xg5srV4a-wRRJu4F2CANpqEfgkdvRgFKq5xvilkIJQM";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Act
            var response = await _httpClient.PostAsync("gateway/product", content);

            //Assert
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Not_Create_Product_By_Unauthorized_User_ProductMicroservice()
        {
            //Arrange

            var model = new Smartphone { Name = "Lenovo B54", Brand = "Lenovo", Price = 1234 };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");
            string token = "incorrect";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Act
            var response = await _httpClient.PostAsync("gateway/product", content);

            //Assert
            Assert.Equal("Unauthorized", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Get_Product_By_Id_ProductMicroservice()
        {
            var response = await _httpClient.GetAsync("gateway/product/1");

            string json = await response.Content.ReadAsStringAsync();
            Smartphone result = JsonConvert.DeserializeObject<Smartphone>(json);

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Add_Smartphone_To_Cart_ProductMicroservice()
        {
            //Act
            var model = new Smartphone { Id = 1, Name = "test", Brand = "test", Price = 1234 };
            int quantity = 2;
            string json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"/gateway/product/cart/{quantity}", content);

            var result = await _httpClient.GetAsync("/gateway/product/cart");
            string jsonCartContent = await result.Content.ReadAsStringAsync();
            var cartLine = JsonConvert.DeserializeObject<List<CartItem>>(jsonCartContent);

            //Assert

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Single(cartLine);
            Assert.Equal(2, cartLine[0].Quantity);
        }

        [Fact]
        public async Task Succsessfull_Login_By_CorrectUserData_In_AuthenticationMicroservice()
        {
            var model = new User { Email = "user1@mail.ru", Password = "123456" };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("gateway/authentication/login", content);
            string token = await response.Content.ReadAsStringAsync();

            Assert.NotNull(token);
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Succsessfull_Create_Order_By_Authorized_User_OrderMicroservice()
        {
            //Arrange
            var model = new Smartphone { Name = "Lenovo B54", Brand = "Lenovo", Price = 1234 };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyMUBtYWlsLnJ1In0.xg5srV4a-wRRJu4F2CANpqEfgkdvRgFKq5xvilkIJQM";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Act
            var response = await _httpClient.PostAsync("gateway/order", content);

            //Assert
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Not_Create_Order_By_Unauthorized_User_ProductMicroservice()
        {
            //Arrange

            var model = new Smartphone { Name = "Lenovo B54", Brand = "Lenovo", Price = 1234 };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");
            string token = "incorrect";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Act
            var response = await _httpClient.PostAsync("gateway/order", content);

            //Assert
            Assert.Equal("Unauthorized", response.StatusCode.ToString());
        }
    }
}
