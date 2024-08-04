using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Database.Entities;

namespace Notebook.EntityFramework.Configuration;

internal sealed class NoteConfiguration : IEntityTypeConfiguration<NoteEntity>
{
    public void Configure(EntityTypeBuilder<NoteEntity> builder)
    {
        builder.ToTable(DatabaseTableNames.NOTES_TABLE_NAME).HasKey(note => note.Id);

        builder.HasOne<UserEntity>(note => note.Owner)
            .WithMany(user => user.Notes)
            .HasForeignKey(note => note.OwnerId);
    }
}