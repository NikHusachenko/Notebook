using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using Notebook.Services.CryptingServices;
using Notebook.Services.ResultService;
using System.Data;

namespace Notebook.Handler.Comment.Update;

public sealed class UpdateCommentHandler(
    IGenericRepository<CommentEntity> repository,
    ICryptingManager cryptingManager)
    : IRequestHandler<UpdateCommentRequest, Result<CommentEntity>>
{
    private const string NotFoundError = "Comment not found.";
    private const string UpdateError = "Error while updating comment content.";

    public async Task<Result<CommentEntity>> Handle(UpdateCommentRequest request, CancellationToken cancellationToken)
    {
        CommentEntity? dbRecord = await repository.GetById(request.Id);
        if (dbRecord is null)
        {
            return Result<CommentEntity>.Error(NotFoundError);
        }

        dbRecord.Content = cryptingManager.Encrypt(request.Content);

        using (IDbContextTransaction transaction = await repository.NewTransaction())
        {
            try
            {
                await repository.Update(dbRecord);
            }
            catch
            {
                await transaction.RollbackAsync();
                return Result<CommentEntity>.Error(UpdateError);
            }
        }

        return Result<CommentEntity>.Success(dbRecord);
    }
}