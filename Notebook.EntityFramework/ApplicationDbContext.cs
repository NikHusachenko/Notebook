using Microsoft.EntityFrameworkCore;
using Notebook.Database.Entities;
using Notebook.EntityFramework.Configuration;

namespace Notebook.EntityFramework;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<CredentialsEntity> Credentials { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<NoteEntity> Notes { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<UserLikesEntity> UserLikes { get; set; }
    public DbSet<FollowingEntity> Followings { get; set; }
    public DbSet<SessionEntity> Sessions { get; set; }
    public DbSet<TokenEntity> Tokens { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CredentialsConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new NoteConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new UserLikesConfiguration());
        modelBuilder.ApplyConfiguration(new FollowingConfiguration());
        modelBuilder.ApplyConfiguration(new SessionsConfiguration());
        modelBuilder.ApplyConfiguration(new TokenConfiguration());
    }
}