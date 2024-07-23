using MediatR;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Authentication.Invite;

public sealed record InviteRequest(string Email) : IRequest<Result>;