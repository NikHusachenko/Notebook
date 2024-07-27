using MediatR;
using Notebook.Database.Entities;
using Notebook.EntityFramework.GenericRepository;
using Notebook.Services.HashServices;
using Notebook.Services.Jwt;
using Notebook.Services.ResultService;
using System.Security.Claims;

namespace Notebook.Handler.Authentication.SignIn;

public sealed class SignInHandler(
    IGenericRepository<CredentialsEntity> repository,
    IJwtService jwtService)
    : IRequestHandler<SignInRequest, Result<string>>
{
    private const string InvalidCredentialsError = "Invalid credentials.";

    public Task<Result<string>> Handle(SignInRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    private async Task<Result<CredentialsEntity>> VerifyCredentials(string login, string password)
    {
        CredentialsEntity? result = await repository.GetBy(credentials => credentials.Login == login);
        if (result is null)
        {
            return Result<CredentialsEntity>.Error(InvalidCredentialsError);
        }

        if (!Hasher.Verify(result.HashedPassword, password, result.Salt))
        {
            return Result<CredentialsEntity>.Error(InvalidCredentialsError);
        }
        return Result<CredentialsEntity>.Success(result);
    }

    private IEnumerable<Claim> GetClaims(CredentialsEntity credentials) =>
    [
        new Claim(JwtClaimTypes.ID, credentials.Id.ToString()),
        new Claim(JwtClaimTypes.LOGIN, credentials.Login),
        new Claim(JwtClaimTypes.EMAIL, credentials.Email),
        new Claim(JwtClaimTypes.FIRST_NAME, credentials.User.FirstName),
        new Claim(JwtClaimTypes.LAST_NAME, credentials.User.LastName)
    ];

    private Result<string> CreateToken(IEnumerable<Claim> claims) => jwtService.Encode(claims);
}