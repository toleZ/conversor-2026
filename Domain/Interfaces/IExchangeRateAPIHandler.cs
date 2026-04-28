using System;

namespace Domain.Interfaces;

public interface IExchangeRateAPIHandler
{
    Task<string?> GetExchange(string from, string to);

}
