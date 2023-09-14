using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;

namespace Bound.Nugets.AzureADB2C.Local
{
    public static class ADB2CExtensionMethods
    {
        public static void AddAzureADB2CAuthentication(this IServiceCollection services)
        {
            var key = GetKeys();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                          .AddJwtBearer(x =>
                          {
                              x.RequireHttpsMetadata = false;
                              x.SaveToken = true;
                              x.TokenValidationParameters = new TokenValidationParameters
                              {
                                  RequireSignedTokens = true,
                                  ValidateAudience = true,
                                  ValidAudience = "1230147b-e6ef-48d8-92dc-2deb8271121d",
                                  ValidateIssuer = true,
                                  ValidIssuer = key.Issuer,
                                  IssuerSigningKey = key.SigningKeys.ToList()[0],
                                  ValidateLifetime = true,
                              };

                              x.Events = new JwtBearerEvents
                              {
                                  OnMessageReceived = context =>
                                  {
                                      string accessToken = context.Request.Headers["Authorization"];

                                      if (!string.IsNullOrEmpty(accessToken))
                                      {
                                          context.Token = accessToken["Bearer ".Length..];
                                      }

                                      return Task.CompletedTask;
                                  },
                                  OnAuthenticationFailed = context =>
                                  {
                                      if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                      {
                                          context.Response.Headers.Add("Token-Expired", "true");
                                      }

                                      return Task.CompletedTask;
                                  }
                              };
                          });


        }

        static OpenIdConnectConfiguration GetKeys()
        {
            string stsDiscoveryEndpoint = "https://boundtechnologiesadb2c.b2clogin.com/boundtechnologiesadb2c.onmicrosoft.com/v2.0/.well-known/openid-configuration?p=B2C_1_bounduserflow";

            ConfigurationManager<OpenIdConnectConfiguration> configManager =
                new(stsDiscoveryEndpoint,
                new OpenIdConnectConfigurationRetriever(),
                new HttpDocumentRetriever());

            OpenIdConnectConfiguration config = configManager.GetConfigurationAsync().Result;
            return config;
        }


        public static void AddAzureADB2CAuthorizationRoles(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                   policy.RequireClaim("extension_Role", "Admin"));
            });
        }
    }
}
