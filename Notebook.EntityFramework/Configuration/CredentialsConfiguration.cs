using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Database.Entities;

namespace Notebook.EntityFramework.Configuration;

internal sealed class CredentialsConfiguration : IEntityTypeConfiguration<CredentialsEntity>
{
    public void Configure(EntityTypeBuilder<CredentialsEntity> builder)
    {
        builder.ToTable("Credentials").HasKey(credentials => credentials.Id);
    }
}