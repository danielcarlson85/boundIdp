using Bound.IDP.Abstractions.Interfaces;
using Bound.IDP.Abstractions.Models;
using Bound.IDP.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Bound.IDP.IntegrationTests
{
    public class HttpClientServiceFixture
    {

        private IHttpClientService _httpClientService { get; set; }
        public HttpStatusCode StatusCode { get; set; }


        public HttpClientServiceFixture()
        {
            var serviceProvider = Setup();
            _httpClientService = serviceProvider.GetRequiredService<IHttpClientService>();

        }

        public async Task ArrangeForSuccessfulLogin()
        {
            var loginCredentials = CreateLoginCredentialsObject();
            var result = await _httpClientService.MakeLoginRequestAsync(loginCredentials);

            StatusCode = result.StatusCode;
        }

        private static ServiceProvider Setup()
        {
            string appsettingsPath = Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "IDP.Runtime\\appsettings.json");
            IConfiguration Configuration = new ConfigurationBuilder().AddJsonFile(appsettingsPath, optional: false).Build();

            var services = new ServiceCollection();
            services.AddSingleton(Configuration);
            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddTransient<IRestClient, RestClient>();

            return services.BuildServiceProvider();
        }
        private static LoginCredentials CreateLoginCredentialsObject()
        {
            return new LoginCredentials() { Email = "test@boundtechnologies.com", Password = "Dej858591" };
        }
    }
}
