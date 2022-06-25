using Microsoft.AspNetCore.Mvc.Testing;
using MobileStore.Product;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MobileStore.IntegrationTest.AccountTests
{
    public class AccessByTokenTest
    {
        private readonly HttpClient _httpClient;
        
        public AccessByTokenTest()
        {
            _httpClient = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { }).CreateClient();
        }

        [Fact]
        public async Task Кeturn_Unauthorized_When_Invalid_Token()
        {
            string token = "c84f18a2-c6c7-4850-be15-93f9cbaef3b3";
            HttpContent content = new StringContent(JsonConvert.SerializeObject(token),
                Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/product", content);

            Assert.Equal("Unauthorized", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Succsessfull_Access_By_Token()
        {
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyMUBtYWlsLnJ1In0.xg5srV4a-wRRJu4F2CANpqEfgkdvRgFKq5xvilkIJQM";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync("api/product", null);

            Assert.Equal("UnsupportedMediaType", response.StatusCode.ToString());
        }
    }
}
