using Application.Services;
using Domain.Interfaces;
using Infrastructure.Configuration;
using Infrastructure.ExternalServices;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

#region Injections

builder.Services.AddOptions<ExchangeRateApiSettings>()
    .Bind(builder.Configuration.GetSection(ExchangeRateApiSettings.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddHttpClient<IExchangeRateAPIHandler, ExchangeRateAPIHandler>((sp, client) =>
{
    var settings = sp.GetRequiredService<IOptions<ExchangeRateApiSettings>>().Value;
    client.BaseAddress = new Uri($"{settings.BaseUrl}/{settings.Token}/pair/");
})
.AddResilienceHandler("exchange-rate", pipeline =>
{
    // Wait & Retry: 3 intentos con espera exponencial
    pipeline.AddRetry(new HttpRetryStrategyOptions
    {
        MaxRetryAttempts = 3,
        Delay = TimeSpan.FromSeconds(2),
        BackoffType = DelayBackoffType.Exponential,
        UseJitter = true
    });

    // Circuit Breaker: abre el circuito si hay muchos fallos
    pipeline.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions
    {
        SamplingDuration = TimeSpan.FromSeconds(30),
        FailureRatio = 0.5,
        MinimumThroughput = 5,
        BreakDuration = TimeSpan.FromSeconds(15)
    });
});

builder.Services.AddScoped<ExchangeRateAPIService>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
