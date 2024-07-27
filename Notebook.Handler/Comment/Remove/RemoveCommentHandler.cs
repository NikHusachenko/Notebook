using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Comment.Remove;

public sealed class RemoveCommentHandler(IGenericRepository<CommentEntity> repository)
    : IRequestHandler<RemoveCommentRequest, Result>
{
    private const string NotFoundError = "Comment not found.";
    private const string RemovingError = "Error while removing comment.";

    public async Task<Result> Handle(RemoveCommentRequest request, CancellationToken cancellationToken)
    {
        CommentEntity? dbRecord = await repository.GetById(request.Id);
        if (dbRecord is null)
        {
            return Result.Error(NotFoundError);
        }

        using (IDbContextTransaction transaction = await repository.NewTransaction())
        {
            try
            {
                await repository.Delete(dbRecord);
            }
            catch
            {
                await transaction.RollbackAsync();
                return Result.Error(RemovingError);
            }
        }
        return Result.Success();
    }
}