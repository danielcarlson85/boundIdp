using Bound.IDP.Abstractions.Interfaces;
using Bound.IDP.Abstractions.Models;
using Bound.IDP.Managers.Authorization;
using Bound.IDP.Managers.Services;
using Bound.IDP.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Bound.IDP.IntegrationTests
{
    public class ADTokenFixture
    {
        public LoginCredentials LoginCredentials { get; set; }

        public string RefreshTokenResult { get; set; }

        public string AccessToken { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        private IADTokenService _ADTokenService { get; }

        private IHttpClientService _httpClientService { get; set; }

        public ADTokenFixture()
        {
            var serviceProvider = Setup();
            _httpClientService = serviceProvider.GetRequiredService<IHttpClientService>();
            _ADTokenService = serviceProvider.GetRequiredService<IADTokenService>();

        }

        public async Task ArrangeForAccessTokenReturn()
        {
            var loginCredentials = CreateLoginCredentialsObject();
            var result = await _ADTokenService.LoginAsync(loginCredentials);

            AccessToken = result.refresh_token;
        }


        public async Task ArrangeForRefreshTokenReturn()
        {
            var loginCredentials = CreateLoginCredentialsObject();
            var loginResult = await _ADTokenService.LoginAsync(loginCredentials);
            RefreshTokenResult = await _ADTokenService.GetRefreshTokenAsync(loginResult.refresh_token);
        }

        public async Task ArrangeForRefreshTokenRequest()
        {
            var loginCredentials = CreateLoginCredentialsObject();
            var loginResult = await _ADTokenService.LoginAsync(loginCredentials);
            var refreshTokenResult = await _httpClientService.MakeRefreshTokenRequestAsync(loginResult.refresh_token);

            StatusCode = refreshTokenResult.StatusCode;
        }

        private static ServiceProvider Setup()
        {
            string appsettingsPath = Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "IDP.Runtime\\appsettings.json");
            IConfiguration Configuration = new ConfigurationBuilder().AddJsonFile(appsettingsPath, optional: false).Build();

            var services = new ServiceCollection();
            services.AddSingleton(Configuration);
            services.AddScoped<IADTokenService, ADTokenService>();
            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddTransient<IRestClient, RestClient>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<JwtSecurityTokenHandler>();

            return services.BuildServiceProvider();
        }

        private static LoginCredentials CreateLoginCredentialsObject()
        {
            return new LoginCredentials() { Email = "test@boundtechnologies.com", Password = "Dej858591" };
        }
    }
}
