using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Database.Entities;

namespace Notebook.EntityFramework.Configuration;

internal sealed class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable(DatabaseTableNames.USERS_TABLE_NAME).HasKey(user => user.Id);

        builder.HasOne(user => user.Credentials)
            .WithOne(credentials => credentials.User)
            .HasForeignKey<UserEntity>(user => user.CredentialsId);

        builder.Property(user => user.FirstName).HasMaxLength(ConfigurationConstrains.FIRST_NAME_MAX_LENGTH);
        builder.Property(user => user.LastName).HasMaxLength(ConfigurationConstrains.LAST_NAME_MAX_LENGTH);
    }
}