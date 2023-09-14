using Bound.IDP.Abstractions.Models;
using RestSharp;
using System.Threading.Tasks;

namespace Bound.IDP.Abstractions.Interfaces
{
    public interface IHttpClientService
    {
        Task<IRestResponse> MakeLoginRequestAsync(LoginCredentials loginCredentials);
        Task<IRestResponse> MakeRefreshTokenRequestAsync(string refreshToken);
    }
}