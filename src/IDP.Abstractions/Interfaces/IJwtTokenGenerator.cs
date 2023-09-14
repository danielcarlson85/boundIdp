using Bound.IDP.Abstractions.Models.AzureADB2C.User;
using System.IdentityModel.Tokens.Jwt;

namespace Bound.IDP.Abstractions.Interfaces
{
    public interface IJwtTokenGenerator
    {
        ADUserResponse GetAzureADUserFromToken(string token);
        string GetUserObjectIdFromToken(string token);
        JwtSecurityToken DecodeToken(string token);
    }
}