using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using StreamService.Business;
using StreamService.Business.Abstract;
using StreamService.Business.Concrete;
using StreamService.Core.Entities.Constants;
using StreamService.Core.Middleware;
using StreamService.DataAccess;
using StreamService.DataAccess.Abstract;
using StreamService.DataAccess.Concrete.Context.MongoDb;
using StreamService.Entities.Concrete;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// builder
//     .Services.AddControllers()
//     .AddNewtonsoftJson(options =>
//     {
//         options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
//     });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();

    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "QwertyBox API",
            Version = "v1",
            Description = "QwertyBox Streaming Service API",
            Contact = new OpenApiContact
            {
                Name = "Destek Ekibi",
                Email = "destek@ornek.com",
                Url = new Uri("https://ornek.com"),
            },
            License = new OpenApiLicense { Name = "MIT License", Url = new Uri("https://opensource.org/licenses/MIT") },
        }
    );

    options.SwaggerDoc(
        "v2",
        new OpenApiInfo
        {
            Title = "QwertyBox API",
            Version = "v2",
            Description = "QwertyBox Streaming Service API",
            Contact = new OpenApiContact
            {
                Name = "Destek Ekibi",
                Email = "destek@ornek.com",
                Url = new Uri("https://ornek.com"),
            },
            License = new OpenApiLicense { Name = "MIT License", Url = new Uri("https://opensource.org/licenses/MIT") },
        }
    );

    options.AddServer(new OpenApiServer { Url = "/api", Description = "API Gateway" });
});

// Register JWT services
builder.Services.RegisterJwtServices(configuration);

// Register ITokenGenerator service
builder.Services.AddSingleton<ITokenGenerator, TokenGenerator>();

// business service registration
builder.Services.RegisterBusinessServices();

// db access service registration
var mongoDbSettings =
    configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>() ?? throw new InvalidOperationException("MongoDbSettings are not configured.");
builder.Services.RegisterDataAccessServices(mongoDbSettings);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Benim API'm v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "Benim API'm v2");
        options.DocumentTitle = "QwertyBox API Dokümantasyonu";
    });
}

app.UseHttpsRedirection();

app.UseAuthentication(); // JWT authentication middleware
app.UseAuthorization();

app.UseCors(); // Enable CORS

app.MapControllers();

app.Run();
