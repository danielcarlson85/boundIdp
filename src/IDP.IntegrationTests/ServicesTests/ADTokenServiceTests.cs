using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Bound.IDP.IntegrationTests.ServicesTests
{
    public class ADTokenServiceTests : IClassFixture<ADTokenFixture>
    {
        private readonly ADTokenFixture _fixture;

        public ADTokenServiceTests(ADTokenFixture aDTokenFixture)
        {
            _fixture = aDTokenFixture;
        }

        [Fact]
        public async Task EnsureLoginDoesNotReturnNullAccessToken()
        {
            await _fixture.ArrangeForAccessTokenReturn();

            _fixture.AccessToken.Should().NotBeNull();
        }

        [Fact]
        public async Task EnsureGetRefreshTokenDoesNotReturnNullToken()
        {
            await _fixture.ArrangeForRefreshTokenReturn();

           _fixture.RefreshTokenResult.Should().NotBeNull();
        }

        [Fact]
        public async Task EnsureGetRefreshTokenRequestIsValid()
        {
            await _fixture.ArrangeForRefreshTokenRequest();

            _fixture.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}

