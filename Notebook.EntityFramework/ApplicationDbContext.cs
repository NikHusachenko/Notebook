using Microsoft.EntityFrameworkCore;
using Notebook.Database.Entities;
using Notebook.EntityFramework.Configuration;

namespace Notebook.EntityFramework;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<CredentialsEntity> Credentials { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CredentialsConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}