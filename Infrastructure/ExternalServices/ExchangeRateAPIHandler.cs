using System;
using System.Text.Json;
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
            BaseAddress = new Uri($"https://v6.exchangerate-api.com/v6/{AuthenticationToken}/pair/")
        };
    }

    public async Task<decimal?> GetExchange(string from, string to, decimal amount)
    {
        HttpResponseMessage httpResponseMessage = await exchangeRateApiClient.GetAsync($"{from}/{to}/{amount}");
        string json = await httpResponseMessage.Content.ReadAsStringAsync();

        var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("conversion_result").GetDecimal();
    }

}
