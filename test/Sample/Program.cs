using Microsoft.Extensions.ServiceDiscovery.AgileConfig;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient("test", t =>
{
    t.BaseAddress = new Uri($"http://{builder.Environment.ApplicationName.ToLower()}");
}).AddServiceDiscovery();
// Add services to the container.
builder.Services.AddServiceDiscovery()
    .AddAgileConfigServiceEndpointProvider();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.AddAgileConfigRegisterService();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();


app.UseAuthorization();

app.MapControllers();

app.Run();
