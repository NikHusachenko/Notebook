using MediatR;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using Notebook.Services.EmailServices;
using Notebook.Services.Flows;
using Notebook.Services.Jwt;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Authentication.Invite;

public sealed class InviteHandler(
    IGenericRepository<CredentialsEntity> credentialsRepository,
    IGenericRepository<TokenEntity> tokenRepository,
    IJwtService jwtService,
    UrlBuilder urlBuilder,
    EmailMessageManager emailMessageManager,
    IEmailService emailService,
    UserAccessFlow userAccessFlow)
    : IRequestHandler<InviteRequest, Result>
{
    public async Task<Result> Handle(InviteRequest request, CancellationToken cancellationToken)
    {
        return await userAccessFlow.InviteNewUser(request.Email,
            await credentialsRepository.NewTransaction(),
            jwtService.RandomToken,
            credentialsRepository.Create,
            tokenRepository.Create,
            urlBuilder.BuildInviteUrl,
            emailMessageManager.InviteTemplate,
            emailService.SendEmail);
    }
}