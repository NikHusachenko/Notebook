using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using Notebook.Services.CryptingServices;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Comment.Write;

public sealed class WriteCommentHandler(
    IGenericRepository<CommentEntity> repository,
    ICryptingManager cryptingManager)
    : IRequestHandler<WriteCommentRequest, Result<CommentEntity>>
{
    private const string WriteError = "Error while writing comment.";

    public async Task<Result<CommentEntity>> Handle(WriteCommentRequest request, CancellationToken cancellationToken)
    {
        CommentEntity comment = new CommentEntity()
        {
            Content = cryptingManager.Encrypt(request.Content),
            NoteId = request.NoteId,
            UserId = request.AuthorId,
        };

        using (IDbContextTransaction transaction = await repository.NewTransaction())
        {
            try
            {
                await repository.Create(comment);
            }
            catch
            {
                await transaction.RollbackAsync();
                return Result<CommentEntity>.Error(WriteError);
            }
        }
        
        return Result<CommentEntity>.Success(comment);
    }
}