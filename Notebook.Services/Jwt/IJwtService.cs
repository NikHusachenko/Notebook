using Notebook.Services.ResultService;
using System.Security.Claims;

namespace Notebook.Services.Jwt;

public interface IJwtService
{
    Result<string> Encode(IEnumerable<Claim> claims);
    Result<IEnumerable<Claim>> Decode(string token);
}