

using TaskFlow.Infrastructure.Extensions;
using TaskFlow.Application.Extensions;
using TaskFlow.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Configurar o Serilog usando a extensão
builder.Host.UseSerilogLogging();
// Registrar o MediatR e os casos de uso

// Registrar os serviços do MediatR
builder.Services.AddMediatorServices();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar serviços da camada de Infrastructure
builder.Services.AddInfrastructureServices();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();