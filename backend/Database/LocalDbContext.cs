using Backend.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Backend.Database;

public class LocalDbContext : DbContext
{
    public LocalDbContext(DbContextOptions options) : base(options)
    {}
    public DbSet<User> users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToCollection("users");
    }
}