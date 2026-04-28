using System;
using Domain.Interfaces;

namespace Infrastructure.ExternalServices;

public class ExchangeRateAPIHandler : IExchangeRateAPIHandler
{
    private string AuthenticationToken { get; set; } = "d6b23e4e743deea63560e8a7";
    private HttpClient exchangeRateApiClient;
    public ExchangeRateAPIHandler()
    {
        exchangeRateApiClient = new()
        {
            BaseAddress = new Uri($"https://v6.exchangerate-api.com/v6/{AuthenticationToken}/latest/")
        };
    }

    public async Task<string?> GetExchange(string from, string to)
    {
        HttpResponseMessage httpResponseMessage = await exchangeRateApiClient.GetAsync($"{from}");
        return httpResponseMessage.Content.ToString();
    }

}
