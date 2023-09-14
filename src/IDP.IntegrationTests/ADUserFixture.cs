using Bound.IDP.Abstractions.Interfaces;
using Bound.IDP.Runtime.ExtensionMethods;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using System.IO;
using System.Threading.Tasks;

namespace Bound.IDP.IntegrationTests
{
    public class ADUserFixture
    {
        public IUserService UserService { get; }
        public User CreatedUser { get; set; }
        public User DeletedUser { get; set; }
        public User UpdatedUser { get; set; }

        public const string Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6Ilg1ZVhrNHh5b2pORnVtMWtsMll0djhkbE5QNC1jNTdkTzZRR1RWQndhTmsifQ.eyJpc3MiOiJodHRwczovL2JvdW5kdGVjaG5vbG9naWVzYWRiMmMuYjJjbG9naW4uY29tLzFiYzU0MzlkLTU1MmMtNGQ4Yy1hMTUzLTVhMTllMTc5MDczNC92Mi4wLyIsImV4cCI6MTYxMzU5MDMzOCwibmJmIjoxNjEzNTg2NzM4LCJhdWQiOiIxMjMwMTQ3Yi1lNmVmLTQ4ZDgtOTJkYy0yZGViODI3MTEyMWQiLCJpZHAiOiJMb2NhbEFjY291bnQiLCJvaWQiOiIzNDgxMmJkNi1jOGYxLTQ0ZjEtYjEzNy0yZDM3MjQ2MTEwZGEiLCJzdWIiOiIzNDgxMmJkNi1jOGYxLTQ0ZjEtYjEzNy0yZDM3MjQ2MTEwZGEiLCJnaXZlbl9uYW1lIjoidXNlciIsImZhbWlseV9uYW1lIjoidXNlciIsIm5hbWUiOiJ1c2VyQGJvdW5kdGVjaG5vbG9naWVzLmNvbSIsIm5ld1VzZXIiOmZhbHNlLCJjaXR5IjoidXNlciIsImNvdW50cnkiOiJ1c2VyIiwiam9iVGl0bGUiOiJ1c2VyIiwicG9zdGFsQ29kZSI6InVzZXIiLCJzdGF0ZSI6InVzZXIiLCJzdHJlZXRBZGRyZXNzIjoidXNlciIsImV4dGVuc2lvbl9Sb2xlIjoiVXNlciIsImVtYWlscyI6WyJ1c2VyQGJvdW5kdGVjaG5vbG9naWVzLmNvbSJdLCJ0ZnAiOiJCMkNfMV9ib3VuZHVzZXJmbG93IiwiYXpwIjoiMTIzMDE0N2ItZTZlZi00OGQ4LTkyZGMtMmRlYjgyNzExMjFkIiwidmVyIjoiMS4wIiwiaWF0IjoxNjEzNTg2NzM4fQ.eBJ0LtBPgiixnl5WRcOv32E95lK6ZqQ32bb_2QXAaiadCxBp9MBXgZc1AjbKDmCP_uvLOnPWn2v3ggY2nh4jgsDdn6vCafDX-XrXRAnZiVbI2RlNVLlUa907vAODvyBhTGV1IA0AltbEYNGJmd2Pg4cnrVLoiOSNY1k3g9SmQdZUUPA2t7R8qUMVsE7YT5yhMCX3Llq8xefnB5_wxEOvL9XzMo6qtGiFXY_svMH8xuzxLS83_Zy2Oj-ELXqmFAbcH-Z3cbEj7hP1-vJuYnfy_CGNoQ7GbE4zjI1BFUDlKRE3-YoxizGlpn59FxhxchVDJRwom2B-FiYjHC-bid-cmw";

        public ADUserFixture()
        {
            var serviceProvider = Setup();
            UserService = serviceProvider.GetRequiredService<IUserService>();
        }

        public async Task ArrangeForCreateUser()
        {
            var user = CreateNewUser();

            await UserService.CreateUserAsync(user, "testPassword@", "TestRole");

            CreatedUser = await UserService.GetUserAsync("integrationTest@boundtechnologies.com");

        }

        public async Task ArrangeForDeleteUser()
        {
            var user = await UserService.GetUserAsync("integrationTest@boundtechnologies.com");

            await UserService.DeleteUserAsync(user.Id);

            DeletedUser = await UserService.GetUserAsync("integrationTest@boundtechnologies.com");
        }

        public async Task ArrangeForUpdatedUser()
        {
            var user = await UserService.GetUserAsync("integrationTest@boundtechnologies.com");

            var userToUpdate = CreateNewUser();

            userToUpdate.Mail = "updatedTest@boundtechnologies.com";
            userToUpdate.GivenName = "UPDATED";
            userToUpdate.Surname = "UPDATED";
            userToUpdate.City = "UPDATED";
            userToUpdate.MobilePhone = "000";
            userToUpdate.PostalCode = "000";
            userToUpdate.DisplayName = "updatedTest@boundtechnologies.com";
            userToUpdate.EmployeeId = "000";
            userToUpdate.State = "UPDATED";
            userToUpdate.Country = "UPDATED";
            userToUpdate.CompanyName = "UPDATED";
            userToUpdate.JobTitle = "UPDATED";
            userToUpdate.Id = user.Id;

            await UserService.UpdateUserAsync(userToUpdate, "TestRole");

            UpdatedUser = await UserService.GetUserAsync("integrationTest@boundtechnologies.com");

        }

        private static ServiceProvider Setup()
        {
            string appsettingsPath = Path.Combine(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "IDP.Runtime\\appsettings.json");
            IConfiguration Configuration = new ConfigurationBuilder().AddJsonFile(appsettingsPath, optional: false).Build();

            var services = new ServiceCollection();
            services.AddSingleton(Configuration);
            services.AddGraphUserAPI(Configuration);

            return services.BuildServiceProvider();
        }
        private static User CreateNewUser()
        {
            return new User()
            {
                Mail = "integrationTest@boundtechnologies.com"
            };
        }
    }
}
