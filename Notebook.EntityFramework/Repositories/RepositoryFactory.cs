namespace Notebook.EntityFramework.Repositories;

public sealed class RepositoryFactory(ApplicationDbContext dbContext) : IRepositoryFactory
{
    public CredentialsRepository NewCredentialsRepository() => new(dbContext);
    public UserRepository NewUserRepository() => new(dbContext);
    public NoteRepository NewNoteRepository() => new(dbContext);
    public UserLikesRepository NewUserLikesRepository() => new(dbContext);
    public CommentRepository NewCommentRepository() => new(dbContext);
    public TokenRepository NewTokenRepository() => new(dbContext);
}