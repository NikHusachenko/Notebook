using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Database.Entities;

namespace Notebook.EntityFramework.Configuration;

internal sealed class CredentialsConfiguration : IEntityTypeConfiguration<CredentialsEntity>
{
    public void Configure(EntityTypeBuilder<CredentialsEntity> builder)
    {
        builder.ToTable(DatabaseTableNames.CREDENTIALS_TABLE_NAME).HasKey(credentials => credentials.Id);

        builder.Property(credentials => credentials.Email).HasMaxLength(ConfigurationConstrains.EMAIL_MAX_LENGTH);
        builder.Property(credentials => credentials.Login).HasMaxLength(ConfigurationConstrains.LOGIN_MAX_LENGHT);
    }
}