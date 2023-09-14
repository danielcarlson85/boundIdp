using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Bound.IDP.IntegrationTests.ServicesTests
{
    public class HttpClientServiceTests : IClassFixture<HttpClientServiceFixture>
    {
        private readonly HttpClientServiceFixture _fixture;

        public HttpClientServiceTests(HttpClientServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task EnsureLoginRequestIsSuccessful()
        {
            await _fixture.ArrangeForSuccessfulLogin();

            _fixture.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}