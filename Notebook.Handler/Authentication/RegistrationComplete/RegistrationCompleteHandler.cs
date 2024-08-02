using MediatR;
using Microsoft.EntityFrameworkCore;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using Notebook.Services.AuthenticationServices;
using Notebook.Services.Flows;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Authentication.RegistrationComplete;

public sealed class RegistrationCompleteHandler(
    IGenericRepository<TokenEntity> tokenRepository,
    IGenericRepository<UserEntity> userRepository,
    IGenericRepository<CredentialsEntity> credentialsRepository,
    ISessionManager sessionManager,
    UserAccessFlow accessFlow)
    : IRequestHandler<RegistrationCompleteRequest, Result>
{
    private const string TokenNotFoundError = "Token not found.";

    public async Task<Result> Handle(RegistrationCompleteRequest request, CancellationToken cancellationToken) =>
        await accessFlow.RegistrationComplete(
            request.Token,
            request.FirstName,
            request.LastName,
            request.Login,
            request.Password,
            await credentialsRepository.NewTransaction(),
            ValidateTokenAndGetCredentials,
            userRepository.Create,
            credentialsRepository.Update,
            sessionManager.Append
        );

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