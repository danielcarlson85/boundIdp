using Bound.IDP.Abstractions.Constants;
using Bound.IDP.Abstractions.Interfaces;
using Bound.IDP.Abstractions.Models;
using Bound.IDP.Abstractions.Models.Settings;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Bound.IDP.Runtime
{
    public class HttpClientService : IHttpClientService
    {
        private readonly ADAuthSettings _azureADB2CSettings;
        private readonly IRestClient _restClient;

        public HttpClientService(IConfiguration configuration, IRestClient restClient)
        {
            _azureADB2CSettings = configuration.GetSection("ADAuthSettings").Get<ADAuthSettings>();
            _restClient = restClient;
        }

        public async Task<IRestResponse> MakeLoginRequestAsync(LoginCredentials loginCredentials)
        {
            _restClient.BaseUrl = new Uri(_azureADB2CSettings.BaseAddress);
            var request = new RestRequest(Method.POST);
            request.AddHeader(ADConstants.ADUserRequest.ContentType, _azureADB2CSettings.ContentType);
            request.AddParameter(ADConstants.ADUserRequest.Username, loginCredentials.Email);
            request.AddParameter(ADConstants.ADUserRequest.Password, loginCredentials.Password);
            request.AddParameter(ADConstants.ADUserRequest.GrantType, _azureADB2CSettings.GrantType);
            request.AddParameter(ADConstants.ADUserRequest.Scope, _azureADB2CSettings.Scope);
            request.AddParameter(ADConstants.ADUserRequest.ClientId, _azureADB2CSettings.ClientId);
            request.AddParameter(ADConstants.ADUserRequest.ResponseType, _azureADB2CSettings.ResponseType);
            var response = await _restClient.ExecuteAsync(request);

            return response;
        }

        public async Task<IRestResponse> MakeRefreshTokenRequestAsync(string refreshToken)
        {
            _restClient.BaseUrl = new Uri(_azureADB2CSettings.BaseAddress);
            var request = new RestRequest(Method.POST);
            request.AddParameter(ADConstants.ADRefreshTokenRequest.GrantType, ADConstants.ADRefreshTokenRequest.RefreshToken);
            request.AddParameter(ADConstants.ADRefreshTokenRequest.ResponseType, ADConstants.ADRefreshTokenRequest.IdToken);
            request.AddParameter(ADConstants.ADRefreshTokenRequest.ClientId, _azureADB2CSettings.ClientId);
            request.AddParameter(ADConstants.ADRefreshTokenRequest.Resource, _azureADB2CSettings.ClientId);
            request.AddParameter(ADConstants.ADRefreshTokenRequest.RefreshToken, refreshToken);
            var response = await _restClient.ExecuteAsync(request);

            return response;
        }
    }
}