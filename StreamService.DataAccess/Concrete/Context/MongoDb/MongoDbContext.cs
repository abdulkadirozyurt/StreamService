using System;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using StreamService.Entities.Concrete;

namespace StreamService.DataAccess.Concrete.Context.MongoDb;

// primary constructor
public class MongoDbContext(DbContextOptions<MongoDbContext> options, MongoDbSettings settings) : DbContext(options)
{
    private readonly MongoDbSettings _settings = settings ?? throw new ArgumentNullException(nameof(settings));

    public DbSet<StreamInformation> StreamInformations { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToCollection("users");
        modelBuilder.Entity<StreamInformation>().ToCollection("streamInformations");
    }
}
