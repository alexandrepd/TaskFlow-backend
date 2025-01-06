using TaskFlow.Infrastructure.Extensions;
using TaskFlow.Application.Extensions;
using TaskFlow.API.Extensions;
using MediatR;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Serilog;
using TaskFlow.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

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
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });


    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TaskFlow.API",
        Version = "v1"

    });
}
);

// Registrar serviços da camada de Infrastructure
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.ClearProviders();

var app = builder.Build();

// Inicializar o banco de dados
DatabaseInitializer.Initialize(app.Services);

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.Run();