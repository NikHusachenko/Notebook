using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Database.Entities;

namespace Notebook.EntityFramework.Configuration;

internal sealed class FollowingConfiguration : IEntityTypeConfiguration<FollowingEntity>
{
    public void Configure(EntityTypeBuilder<FollowingEntity> builder)
    {
        builder.ToTable("Followings").HasKey(f => new { f.FollowerId, f.FollowingId });

        // User has followers (Follower)
        builder.HasOne<UserEntity>(f => f.Follower)
            .WithMany(user => user.Followers)
            .HasForeignKey(f => f.FollowerId)
            .OnDelete(DeleteBehavior.Cascade);

        // User has followings (Following)
        builder.HasOne<UserEntity>(f => f.Following)
            .WithMany(user => user.Followings)
            .HasForeignKey(f => f.FollowingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}