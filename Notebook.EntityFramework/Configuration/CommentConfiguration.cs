using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Database.Entities;

namespace Notebook.EntityFramework.Configuration;

internal sealed class CommentConfiguration : IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.ToTable(DatabaseTableNames.USER_COMMENTS_TABLE_NAME).HasKey(comment => comment.Id);

        builder.HasOne<UserEntity>(comment => comment.User)
            .WithMany(user => user.Comments)
            .HasForeignKey(comment => comment.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne<NoteEntity>(comment => comment.Note)
            .WithMany(note => note.Comments)
            .HasForeignKey(comment => comment.NoteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(comment => comment.Content).HasMaxLength(ConfigurationConstrains.COMMENT_CONTENT_MAX_LENGTH);
    }
}