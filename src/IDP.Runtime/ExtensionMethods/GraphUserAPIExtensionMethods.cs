using Bound.IDP.Abstractions.Interfaces;
using Bound.IDP.Abstractions.Models.Settings;
using Bound.IDP.Managers.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;

namespace Bound.IDP.Runtime.ExtensionMethods
{
    public static class GraphUserAPI
    {
        public static void AddGraphUserAPI(this IServiceCollection services, IConfiguration configuration)
        {
            var adGraphSettings = configuration.GetSection("ADGraphSettings").Get<ADGraphSettings>();

            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(adGraphSettings.GraphAppId)
                .WithTenantId(adGraphSettings.TenantId)
                .WithClientSecret(adGraphSettings.GraphClientSecret)
                .Build();

            ClientCredentialProvider authProvider = new(confidentialClientApplication);

            services.AddScoped<IUserService, UserService>(_ => new UserService(new GraphServiceClient(authProvider), adGraphSettings));
        }
    }
}