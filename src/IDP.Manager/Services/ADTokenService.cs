using Bound.IDP.Abstractions.Interfaces;
using Bound.IDP.Abstractions.Models;
using Bound.IDP.Abstractions.Models.AzureADB2C.Tokens;
using Bound.IDP.Abstractions.Models.AzureADB2C.User;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bound.IDP.Managers.Services
{
    public class ADTokenService : IADTokenService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public ADTokenService(IHttpClientService httpClientService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _httpClientService = httpClientService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        /// <summary>
        /// Calls Azure AD B2C to validate credentials for authentication and generates tokens.
        /// </summary>
        /// <param name="loginCredentials">The supplied users login credentials</param>
        /// <returns></returns>
        public async Task<ADUserResponse> LoginAsync(LoginCredentials loginCredentials)
        {
            var response = await _httpClientService.MakeLoginRequestAsync(loginCredentials);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var azureADB2CUserToken = JsonSerializer.Deserialize<ADUserTokenResponse>(response.Content);
                var azureUserResponse = _jwtTokenGenerator.GetAzureADUserFromToken(azureADB2CUserToken.access_token);
                azureUserResponse.refresh_token = azureADB2CUserToken.refresh_token;
                azureUserResponse.access_token = azureADB2CUserToken.access_token;

                return azureUserResponse;
            }
            else
            {
                var azureADB2CError = JsonSerializer.Deserialize<ADTokenErrorResponse>(response.Content);
                throw new Exception(azureADB2CError.error_description);
            }
        }

        /// <summary>
        /// Calls Azure AD B2C to validate credentials and generates a new access token.
        /// </summary>
        /// <param name="refreshToken">The supplied refreshToken</param>
        /// <returns></returns>
        public async Task<string> GetRefreshTokenAsync(string refreshToken)
        {
            var response = await _httpClientService.MakeRefreshTokenRequestAsync(refreshToken);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var azureADB2CUserToken = JsonSerializer.Deserialize<ADRefreshTokenResponse>(response.Content);
                return azureADB2CUserToken.access_token;
            }
            else
            {
                var azureADB2CError = JsonSerializer.Deserialize<ADTokenErrorResponse>(response.Content);
                throw new Exception(azureADB2CError.error_description);
            }
        }
    }
}