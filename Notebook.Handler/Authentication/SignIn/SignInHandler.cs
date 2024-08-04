using MediatR;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using Notebook.Services.AuthenticationServices;
using Notebook.Services.Flows;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Authentication.SignIn;

public sealed class SignInHandler(
    IGenericRepository<CredentialsEntity> credentialsRepository,
    ISessionManager sessionManager,
    UserAccessFlow accessFlow)
    : IRequestHandler<SignInRequest, Result>
{
    private const string TokenNotFoundError = "Token not found.";

    public async Task<Result> Handle(SignInRequest request, CancellationToken cancellationToken) =>
        await accessFlow.Authentication(request.Login,
            request.Password,
            async login => await credentialsRepository.GetBy(entity => entity.Login == login),
            sessionManager.Append);
}