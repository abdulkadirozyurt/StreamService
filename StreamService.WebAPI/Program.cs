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
builder.Services.AddSwaggerGen();

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
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // JWT authentication middleware
app.UseAuthorization();

app.UseCors(); // Enable CORS

app.MapControllers();

app.Run();
