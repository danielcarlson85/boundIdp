using AutoMapper;
using Bound.EventBus;
using Bound.IDP.Abstractions.Constants;
using Bound.IDP.Abstractions.Interfaces;
using Bound.IDP.Abstractions.Models.Settings;
using Bound.IDP.Managers.Authorization;
using Bound.IDP.Managers.Mapping;
using Bound.IDP.Managers.Services;
using Bound.IDP.Runtime.ExtensionMethods;
using Bound.Nugets.AzureADB2C.Local;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;

namespace Bound.IDP.Runtime
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerConfig();

            services.AddAzureADB2CAuthentication();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                   policy.RequireClaim("extension_Role", "Admin"));
            });

            services.AddControllers();

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
            services.AddSingleton(mappingConfig.CreateMapper());
            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IADTokenService, ADTokenService>();
            services.AddTransient<IRestClient,  RestClient>();
            services.AddScoped<JwtSecurityTokenHandler>();

           // services.Configure<EventBusSettings>(options => _configuration.GetSection("EventBus").Bind(options));

            var eventBusConnectionString = _configuration["EventBus:ConnectionString"];
            services.AddUserEventBus(eventBusConnectionString);
            services.AddTestEventBus(eventBusConnectionString);
            services.AddGraphUserAPI(_configuration);

            services.AddApplicationInsightsTelemetry(_configuration["APPINSIGHTS_CONNECTIONSTRING"]);
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, IUserEventBusHandler userEventBusHandler, ITestEventBusHandler testEventHubHandler)
        {
            await userEventBusHandler.StartRecieveMessageAsync();
            await testEventHubHandler.StartRecieveMessageAsync();

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint(SwaggerConstants.SwaggerEndpoint, $"{SwaggerConstants.Title} {SwaggerConstants.Version}"));

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}