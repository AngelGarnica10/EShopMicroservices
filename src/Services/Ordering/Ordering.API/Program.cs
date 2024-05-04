using Ordering.API;
using Ordering.Application;
using Ordering.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container

builder.Services
    .AddApplicationServices()
    .AddInfraestructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

// Configure the HTTP request pipeline

app.Run();
