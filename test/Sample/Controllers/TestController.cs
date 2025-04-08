using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(IHttpClientFactory httpClientFactory) : ControllerBase
    {

        private HttpClient httpClient = httpClientFactory.CreateClient("test");

        [HttpGet]
        public async Task<string> Get()
        {
            var result = await httpClient.GetStringAsync("WeatherForecast");
            return result;
        }
    }
}
