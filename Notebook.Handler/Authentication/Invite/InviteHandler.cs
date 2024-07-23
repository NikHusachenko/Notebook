using MediatR;
using Notebook.EntityFramework.Repositories;
using Notebook.Services.EmailServices;
using Notebook.Services.Flows;
using Notebook.Services.Jwt;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Authentication.Invite;

public sealed class InviteHandler(
    IRepositoryFactory repositoryFactory,
    IJwtService jwtService,
    UrlBuilder urlBuilder,
    EmailMessageManager emailMessageManager,
    IEmailService emailService,
    UserAccessFlow userAccessFlow)
    : IRequestHandler<InviteRequest, Result>
{
    public async Task<Result> Handle(InviteRequest request, CancellationToken cancellationToken)
    {
        CredentialsRepository credentialsRepository = repositoryFactory.NewCredentialsRepository();
        TokenRepository tokenRepository = repositoryFactory.NewTokenRepository();

        return await userAccessFlow.InviteNewUser(request.Email,
            jwtService.RandomToken,
            credentialsRepository.Create,
            credentialsRepository.Delete,
            tokenRepository.Create,
            tokenRepository.Delete,
            urlBuilder.BuildInviteUrl,
            emailMessageManager.InviteTemplate,
            emailService.SendEmail);
    }
}

// Create credentials
// Create user
// Generate token
// Save token
// Build url
// Get html template
// Send email