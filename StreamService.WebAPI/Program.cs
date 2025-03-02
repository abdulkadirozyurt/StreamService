using Newtonsoft.Json;
using StreamService.Business;
using StreamService.Business.Abstract;
using StreamService.Business.Concrete;
using StreamService.Core.Middleware;
using StreamService.DataAccess;
using StreamService.DataAccess.Abstract;
using StreamService.DataAccess.Concrete.Context.MongoDb;
using StreamService.DataAccess.Concrete.EntityFramework;
using StreamService.Entities.Concrete;
using StreamService.Core.Entities.Constants;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder
    .Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });
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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleDal = services.GetRequiredService<IRoleDal>();

    var roles = new List<string> { UserRoleConstants.Admin, UserRoleConstants.User };

    foreach (var roleName in roles)
    {
        if (await roleDal.GetByNameAsync(roleName) == null)
        {
            await roleDal.CreateAsync(new Role { Name = roleName });
        }
    }
}

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

app.MapControllers();

app.Run();
