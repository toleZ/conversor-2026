using System;

namespace Domain.Interfaces;

public interface IExchangeRateAPIHandler
{
    Task<decimal?> GetExchange(string from, string to, decimal amount);

}
