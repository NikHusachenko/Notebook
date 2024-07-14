using MediatR;
using Notebook.Database.Entities;
using Notebook.EntityFramework.Repositories;
using Notebook.Services.CryptingServices;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Comment.Write;

public sealed class WriteCommentHandler(
    IRepositoryFactory repositoryFactory,
    ICryptingManager cryptingManager)
    : IRequestHandler<WriteCommentRequest, Result<CommentEntity>>
{
    private const string WriteError = "Error while writing comment.";

    public async Task<Result<CommentEntity>> Handle(WriteCommentRequest request, CancellationToken cancellationToken)
    {
        CommentRepository repository = repositoryFactory.NewCommentRepository();
        CommentEntity comment = new CommentEntity()
        {
            Content = cryptingManager.Encrypt(request.Content),
            NoteId = request.NoteId,
            UserId = request.AuthorId,
        };

        try
        {
            await repository.Create(comment);
        }
        catch
        {
            return Result<CommentEntity>.Error(WriteError);
        }
        return Result<CommentEntity>.Success(comment);
    }
}