using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SH.APIProblemDetails;

namespace Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(IHttpClientFactory httpClientFactory) : ControllerBase
    {

        private HttpClient httpClient = httpClientFactory.CreateClient("test");
        [EndpointSummary("HttpClient 服务发现测试")]
        [HttpGet]
        public async Task<string> Get()
        {
            var result = await httpClient.GetStringAsync("WeatherForecast");
            return result;
        }
        [HttpGet("problemresult")]

        public async Task<IResult> problemresult()
        {
            return Results.Problem(new ProblemDetails
            {

                Title = "我的测试",
                Detail = "测试是否好事----------",
                Type = "自定义类型",
            });
        }
        [HttpGet("problemresult2")]
        public async Task<IResult> problemresult2()
        {
            throw new ProblemDetailsException("异常测试", "我的错误原因！");
        }
    }
}
