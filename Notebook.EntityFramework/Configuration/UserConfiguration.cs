using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Database.Entities;

namespace Notebook.EntityFramework.Configuration;

internal sealed class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users").HasKey(user => user.Id);

        builder.HasOne(user => user.Credentials)
            .WithOne(credentials => credentials.User)
            .HasForeignKey<UserEntity>(user => user.CreedntialsId);
    }
}