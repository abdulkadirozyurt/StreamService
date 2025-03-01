using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StreamService.Business;
using StreamService.Core.Middleware;
using StreamService.DataAccess;
using StreamService.DataAccess.Concrete.Context.MongoDb;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// business service registration
builder.Services.RegisterBusinessServices();

// db access service registration
var mongoDbSettings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
builder.Services.RegisterDataAccessServices(mongoDbSettings);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
