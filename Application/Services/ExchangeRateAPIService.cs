using System;
using Domain.Interfaces;

namespace Application.Services;

public class ExchangeRateAPIService
{
    private readonly IExchangeRateAPIHandler _exchangeRateApiHandler;
    public ExchangeRateAPIService(IExchangeRateAPIHandler theOneAPIHandler)
    {
        _exchangeRateApiHandler = theOneAPIHandler;
    }

    public async Task<decimal?> GetExchange(string from, string to, decimal amount)
    {
        return await _exchangeRateApiHandler.GetExchange(from, to, amount);
    }
}
