using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Database.Entities;

namespace Notebook.EntityFramework.Configuration;

internal sealed class TokenConfiguration : IEntityTypeConfiguration<TokenEntity>
{
    public void Configure(EntityTypeBuilder<TokenEntity> builder)
    {
        builder.ToTable("Tokens").HasKey(token => token.Id);

        builder.HasOne<CredentialsEntity>(token => token.Credentials)
            .WithMany(credentials => credentials.Tokens)
            .HasForeignKey(token => token.CredentialsId);
    }
}