using System.IdentityModel.Tokens.Jwt;

namespace CovidTracking.Services.Authentication
{
    public interface IAuthService
    {
        string Generate(string id);

        JwtSecurityToken Verify(string jwt);
    }
}