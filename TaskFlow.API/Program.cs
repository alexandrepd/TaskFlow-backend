

using TaskFlow.Infrastructure.Extensions;
using TaskFlow.Application.Extensions;
using TaskFlow.API.Extensions;
using TaskFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Configurar o Serilog usando a extensão
builder.Host.UseSerilogLogging();
// Registrar o MediatR e os casos de uso

// Registrar os serviços do MediatR
builder.Services.AddMediatorServices();

// Registrar os handlers do MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar serviços da camada de Infrastructure
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();