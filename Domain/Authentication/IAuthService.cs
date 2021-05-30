using System.IdentityModel.Tokens.Jwt;

namespace Domain.Authentication
{
    public interface IAuthService
    {
        string Generate(string id);

        JwtSecurityToken Verify(string jwt);
    }
}