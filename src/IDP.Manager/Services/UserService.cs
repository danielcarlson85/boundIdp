using Bound.IDP.Abstractions.Interfaces;
using Bound.IDP.Abstractions.Models.Settings;
using Bound.IDP.Managers.Helpers;
using Microsoft.Graph;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bound.IDP.Managers.Services
{
    public class UserService : IUserService
    {
        private readonly GraphServiceClient _graphClient;
        private readonly ADGraphSettings _ADGraphSettings;

        public UserService(GraphServiceClient graphClient, ADGraphSettings ADGraphSettings)
        {
            _graphClient = graphClient;
            _ADGraphSettings = ADGraphSettings;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var result = await _graphClient.Users.Request().GetAsync();
            return result.CurrentPage.ToList();
        }

        public async Task<User> GetUserAsync(string mail)
        {
            var objectId = await GetObjectIdFromUserAsync(mail);

            //TODO : Add more properties:
            var user = await _graphClient.Users[objectId]
                .Request()
                .Select(e => new
                {
                    e.Id,
                    e.DisplayName,
                    e.Mail,
                    e.GivenName,
                    e.Surname,
                    e.MobilePhone,
                    e.JobTitle,
                    e.CompanyName,
                    e.EmployeeId,
                    e.State,
                    e.StreetAddress,
                    e.Country,
                    e.City,
                    e.PostalCode
                })
                .GetAsync();

            return user;
        }

        public async Task CreateUserAsync(User user, string password, string role)
        {
            user = UserHelpers.AddAdditionalSettings(user, password, _ADGraphSettings.TenantId, _ADGraphSettings.GraphB2cExtensionAppClientId, role);

            await _graphClient.Users
            .Request()
            .AddAsync(user);

            //Put a message on servicebus to send message
            //Send mail to user.
        }

        public async Task UpdateUserAsync(User user, string role)
        {
            await _graphClient.Users[user.Id]
               .Request()
               .UpdateAsync(user);
        }

        public async Task DeleteUserAsync(string objectId)
        {
            await _graphClient.Users[objectId]
               .Request()
               .DeleteAsync();
        }

        public async Task<string> GetObjectIdFromUserAsync(string mail)
        {
            var user = await _graphClient.Users
                .Request()
                .Filter($"identities/any(c:c/issuerAssignedId eq '{mail}' and c/issuer eq '{_ADGraphSettings.TenantId}')")
                .GetAsync();

            if (user.CurrentPage.Count == 0)
            {
                return null;
            }

            var id = user.CurrentPage[0].Id;

            return id;
        }

    }
}