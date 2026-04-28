using Application.Services;
using Domain.Interfaces;
using Infrastructure.ExternalServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

#region Injections
builder.Services.AddScoped<IExchangeRateAPIHandler, ExchangeRateAPIHandler>();
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
