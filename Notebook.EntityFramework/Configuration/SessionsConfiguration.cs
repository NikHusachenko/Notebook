using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Database.Entities;

namespace Notebook.EntityFramework.Configuration;

internal sealed class SessionsConfiguration : IEntityTypeConfiguration<SessionEntity>
{
    public void Configure(EntityTypeBuilder<SessionEntity> builder)
    {
        builder.ToTable("Sessions").HasKey(session => session.Id);
    }
}