using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HttpClientCsharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientCsharp.Controllers
{
    [Route("api/v1/bore")]
    [ApiController]
    public class BoreController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BoreController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("activity")]
        public async Task<ActionResult<Bore>> Activity()
        {
            var httpClient = _httpClientFactory.CreateClient("Bore");
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{httpClient.BaseAddress}activity");
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return Ok(response.Content.ReadFromJsonAsync<Bore>());
            }

            return BadRequest(response.ReasonPhrase);
        }
    }
}