using MediatR;
using Notebook.Database.Entities;
using Notebook.EntityFramework.Repositories;
using Notebook.Services.CryptingServices;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Comment.Update;

public sealed class UpdateCommentHandler(
    IRepositoryFactory repositoryFactory,
    ICryptingManager cryptingManager)
    : IRequestHandler<UpdateCommentRequest, Result<CommentEntity>>
{
    private const string NotFoundError = "Comment not found.";
    private const string UpdateError = "Error while updating comment content.";

    public async Task<Result<CommentEntity>> Handle(UpdateCommentRequest request, CancellationToken cancellationToken)
    {
        CommentRepository repository = repositoryFactory.NewCommentRepository();

        CommentEntity? dbRecord = await repository.GetById(request.Id);
        if (dbRecord is null)
        {
            return Result<CommentEntity>.Error(NotFoundError);
        }

        dbRecord.Content = cryptingManager.Encrypt(request.Content);

        try
        {
            await repository.Update(dbRecord);
        }
        catch
        {
            return Result<CommentEntity>.Error(UpdateError);
        }
        return Result<CommentEntity>.Success(dbRecord);
    }
}