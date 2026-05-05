using System;
using System.Text.Json;
using Domain.Interfaces;

namespace Infrastructure.ExternalServices;

public class ExchangeRateAPIHandler : IExchangeRateAPIHandler
{
    private readonly HttpClient _httpClient;

    public ExchangeRateAPIHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal?> GetExchange(string from, string to, decimal amount)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{from}/{to}/{amount}");
        string json = await response.Content.ReadAsStringAsync();

        var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("conversion_result").GetDecimal();
    }
}
