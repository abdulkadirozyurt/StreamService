using System;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using StreamService.Entities.Concrete;

namespace StreamService.DataAccess.Concrete.Context.MongoDb;

// primary constructor
public class MongoDbContext(DbContextOptions<MongoDbContext> options, MongoDbSettings settings) : DbContext(options)
{
    private readonly MongoDbSettings _settings = settings ?? throw new ArgumentNullException(nameof(settings));

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<StreamInformation> StreamInformations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToCollection("users");
        modelBuilder.Entity<Role>().ToCollection("roles");
        modelBuilder.Entity<Subscription>().ToCollection("subscriptions");
        modelBuilder.Entity<RefreshToken>().ToCollection("refreshTokens");
        modelBuilder.Entity<StreamInformation>().ToCollection("streamInformations");

        modelBuilder.Entity<User>().HasOne(u => u.Subscription).WithMany(m => m.Users).HasForeignKey(u => u.SubscriptionId).IsRequired(false);
    }
}
