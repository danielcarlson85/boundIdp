using FluentAssertions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;

namespace Bound.IDP.IntegrationTests.ServicesTests
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class UserServiceTests : IClassFixture<ADUserFixture>
    {

        private readonly ADUserFixture _fixture;

        public UserServiceTests(ADUserFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task EnsureGetUserFromAD()
        {
            var user = await _fixture.UserService.GetUserAsync("test@boundtechnologies.com");

            user.Should().NotBeNull();
            user.Mail.Should().Be("test@boundtechnologies.com");
        }

        [Fact]
        public async Task EnsureGetUsersFromAD()
        {
            var users = await _fixture.UserService.GetUsersAsync();

            users.Should().NotBeNull();
            users.Count.Should().BeGreaterThan(1);
        }

        [Fact, Priority(1)]
        public async Task EnsureUserCreatedInAD()
        {
            await _fixture.ArrangeForCreateUser();

            _fixture.CreatedUser.Should().NotBeNull();
        }

        [Fact, Priority(2)]
        public async Task EnsureUserUpdatedInAd()
        {
            await _fixture.ArrangeForUpdatedUser();

            _fixture.UpdatedUser.Should().NotBeNull();
            _fixture.UpdatedUser.DisplayName.Should().Be("updatedTest@boundtechnologies.com");
            _fixture.UpdatedUser.Mail.Should().Be("updatedTest@boundtechnologies.com");
            _fixture.UpdatedUser.GivenName.Should().Be("UPDATED");
            _fixture.UpdatedUser.Surname.Should().Be("UPDATED");
            _fixture.UpdatedUser.MobilePhone.Should().Be("000");
        }

        [Fact, Priority(3)]
        public async Task EnsureUserDeletedFromAD()
        {
            await _fixture.ArrangeForDeleteUser();

            _fixture.DeletedUser.Id.Should().BeNull();
        }
    }
}