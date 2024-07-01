using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Database.Entities;

namespace Notebook.EntityFramework.Configuration;

internal sealed class UserLikesConfiguration : IEntityTypeConfiguration<UserLikesEntity>
{
    public void Configure(EntityTypeBuilder<UserLikesEntity> builder)
    {
        builder.ToTable(DatabaseTableNames.USER_LIKES_TABLE_NAME).HasKey(ul => new { ul.UserId, ul.NoteId });

        builder.HasOne<UserEntity>(ul => ul.User)
            .WithMany(user => user.Liked)
            .HasForeignKey(ul => ul.UserId);

        builder.HasOne<NoteEntity>(ul => ul.Note)
            .WithMany(note => note.Likes)
            .HasForeignKey(ul => ul.NoteId);
    }
}