using MediatR;
using Microsoft.EntityFrameworkCore;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using Notebook.Services.AuthenticationServices;
using Notebook.Services.Flows;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Authentication.SignIn;

public sealed class SignInHandler(
    IGenericRepository<TokenEntity> tokenRepository,
    ISessionManager sessionManager,
    UserAccessFlow accessFlow)
    : IRequestHandler<SignInRequest, Result>
{
    private const string TokenNotFoundError = "Token not found.";

    public async Task<Result> Handle(SignInRequest request, CancellationToken cancellationToken) =>
        await accessFlow.Authentication(request.Token,
            request.Login,
            request.Password,
            ValidateTokenAndGetCredentials,
            sessionManager.Append);

    private async Task<Result<CredentialsEntity>> ValidateTokenAndGetCredentials(string token)
    {
        TokenEntity? dbRecord = await tokenRepository.GetAll()
                    .Include(token => token.Credentials)
                    .FirstOrDefaultAsync(t => t.Token == token);

        return dbRecord is null ?
            Result<CredentialsEntity>.Error(TokenNotFoundError) :
            Result<CredentialsEntity>.Success(dbRecord.Credentials);
    }
}