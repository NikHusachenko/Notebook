﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Database.Entities;

namespace Notebook.EntityFramework.Configuration;

internal sealed class FollowingConfiguration : IEntityTypeConfiguration<FollowingEntity>
{
    public void Configure(EntityTypeBuilder<FollowingEntity> builder)
    {
        builder.ToTable("Followings").HasKey(f => f.Id);

        // User has followers (Follower)
        builder.HasOne<UserEntity>(f => f.Follower)
            .WithMany(user => user.Followers)
            .HasForeignKey(f => f.FollowerId)
            .OnDelete(DeleteBehavior.NoAction);

        // User has followings (Following)
        builder.HasOne<UserEntity>(f => f.Following)
            .WithMany(user => user.Followings)
            .HasForeignKey(f => f.FollowingId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}