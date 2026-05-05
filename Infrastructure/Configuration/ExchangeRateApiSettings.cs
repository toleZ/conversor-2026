using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Configuration;

public class ExchangeRateApiSettings
{
    public const string SectionName = "ExchangeRateApi";

    [Required]
    public string Token { get; set; } = string.Empty;

    [Required]
    public string BaseUrl { get; set; } = string.Empty;
}
