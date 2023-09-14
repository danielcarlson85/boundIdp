using Bound.IDP.Abstractions.Models;
using Bound.IDP.Abstractions.Models.AzureADB2C.User;
using System.Threading.Tasks;

namespace Bound.IDP.Abstractions.Interfaces
{
    public interface IADTokenService
    {
        Task<ADUserResponse> LoginAsync(LoginCredentials loginCredentials);
        Task<string> GetRefreshTokenAsync(string refreshToken);
    }
}