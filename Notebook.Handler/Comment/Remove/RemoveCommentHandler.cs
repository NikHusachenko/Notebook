using MediatR;
using Notebook.Database.Entities;
using Notebook.EntityFramework.Repositories;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Comment.Remove;

public sealed class RemoveCommentHandler(IRepositoryFactory repositoryFactory)
    : IRequestHandler<RemoveCommentRequest, Result>
{
    private const string NotFoundError = "Comment not found.";
    private const string RemovingError = "Error while removing comment.";

    public async Task<Result> Handle(RemoveCommentRequest request, CancellationToken cancellationToken)
    {
        CommentRepository repository = repositoryFactory.NewCommentRepository();

        CommentEntity? dbRecord = await repository.GetById(request.Id);
        if (dbRecord is null)
        {
            return Result.Error(NotFoundError);
        }

        try
        {
            await repository.Delete(dbRecord);
        }
        catch
        {
            return Result.Error(RemovingError);
        }
        return Result.Success();
    }
}