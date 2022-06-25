
using Microsoft.AspNetCore.Mvc.Testing;
using MobileStore.Domain.Entities;
using MobileStore.Order;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MobileStore.IntegrationTest.OrderTests
{
    public class OrderControllerTest
    {
        private readonly HttpClient _httpClient;

        public OrderControllerTest()
        {
            var webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            _httpClient = webHost.CreateClient();
        }

        [Fact]
        public async Task Succsessfull_Create_Order_By_Authorized_User()
        {    
            //Arrange
            var model = new Ordering { CustomerId = 1,  ProductId = 1, };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyMUBtYWlsLnJ1In0.xg5srV4a-wRRJu4F2CANpqEfgkdvRgFKq5xvilkIJQM";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Act
            var response = await _httpClient.PostAsync("api/order", content);

            //Assert
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task NotSuccsessfull_Create_Order_By_NotAuthorized_User()
        {
            //Arrange
            var model = new Ordering { CustomerId = 1, ProductId = 1, };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");
            string incorrectToken = "eyJzdWIiOiJ1c2VyMUBtYWlsLnJ1In0.xg5srV4a-wRRJu4F2CANpqEfgkdvRgFKq5xvilkIJQM";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", incorrectToken);

            //Act
            var response = await _httpClient.PostAsync("api/order", content);

            //Assert
            Assert.Equal("Unauthorized", response.StatusCode.ToString());
        }
    }
}
