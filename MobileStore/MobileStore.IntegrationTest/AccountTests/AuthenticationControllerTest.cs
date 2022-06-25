using Microsoft.AspNetCore.Mvc.Testing;
using MobileStore.Authentication;
using MobileStore.Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MobileStore.IntegrationTest.AccountTests
{
    public class AuthenticationControllerTest
    {
        private readonly HttpClient _httpClient;

        public AuthenticationControllerTest()
        {
            _httpClient = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { }).CreateClient();
        }

        [Fact]
        public async Task GetToken_Upon_Succsessfull_Authorization()
        {
            var model = new User { Email = "user1@mail.ru", Password = "123456" };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/authentication/login",content);
            string token = await response.Content.ReadAsStringAsync();

            Assert.NotNull(token);
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task NotFound_User_Return_NotFound_StatusCode()
        {
            var model = new User { Email = "unknown@mail.ru", Password = "123456" };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/authentication/login", content);

            Assert.Equal("NotFound", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Invalid_Password_Return_BadRequest_StatusCode()
        {
            var model = new User { Email = "user1@mail.ru", Password = "1234567" };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/authentication/login", content);

            Assert.Equal("BadRequest", response?.StatusCode.ToString());
        }
    }
}
