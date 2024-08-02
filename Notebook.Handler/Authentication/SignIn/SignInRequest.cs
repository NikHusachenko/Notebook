using MediatR;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Authentication.SignIn;

public sealed record SignInRequest(string Token, string Login, string Password) : IRequest<Result>;