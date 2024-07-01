using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Notebook.Services.ResultService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Notebook.Services.Jwt;

public sealed class JwtService : IJwtService
{
    private const string CanWriteTokenError = "Can't write token.";
    private const string CanReadTokenError = "Can't readt token.";

    private readonly JwtOptions _options;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public JwtService(IOptionsMonitor<JwtOptions> optionsMonitor)
    {
        _options = optionsMonitor.CurrentValue;
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public Result<IEnumerable<Claim>> Decode(string token)
    {
        if (!_tokenHandler.CanReadToken(token))
        {
            return Result<IEnumerable<Claim>>.Error(CanReadTokenError);
        }
        return Result<IEnumerable<Claim>>.Success(_tokenHandler.ReadJwtToken(token).Claims);
    }

    public Result<string> Encode(IEnumerable<Claim> claims)
    {
        byte[] byteKey = Encoding.UTF8.GetBytes(_options.Key);
        SecurityKey securityKey = new SymmetricSecurityKey(byteKey);
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddSeconds(_options.Expiration),
            signingCredentials: credentials);

        try
        {
            string token = _tokenHandler.WriteToken(securityToken);
            return Result<string>.Success(token);
        }
        catch
        {
            return Result<string>.Error(CanWriteTokenError);
        }
    }
}