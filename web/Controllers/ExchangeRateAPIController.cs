using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateAPIController : ControllerBase
    {
        private readonly ExchangeRateAPIService _exchanegRateAPIService;

        public ExchangeRateAPIController( ExchangeRateAPIService exchanegRateAPIService)
        {
            _exchanegRateAPIService = exchanegRateAPIService;
        }
        public record ExchangeRequest(string From, string To, decimal Amount);

        [HttpPost("exchange")]
        public async Task<IActionResult> GetExchange([FromBody] ExchangeRequest request)
        {
            return Ok(await _exchanegRateAPIService.GetExchange(request.From, request.To, request.Amount));
        }
    }
}
