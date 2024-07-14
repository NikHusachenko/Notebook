namespace Notebook.EntityFramework.Repositories;

public interface IRepositoryFactory
{
    CredentialsRepository NewCredentialsRepository();
    UserRepository NewUserRepository();
    NoteRepository NewNoteRepository();
    UserLikesRepository NewUserLikesRepository();
    CommentRepository NewCommentRepository();
}