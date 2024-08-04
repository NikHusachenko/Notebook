using MediatR;
using Notebook.Services.ResultService;

namespace Notebook.Handler.Authentication.SignIn;

public sealed record SignInRequest(string Login, string Password) : IRequest<Result>;