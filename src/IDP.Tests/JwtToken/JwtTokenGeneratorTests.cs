using FluentAssertions;
using System.IdentityModel.Tokens.Jwt;
using Xunit;

namespace Bound.IDP.Tests.JwtToken
{
    public class JwtTokenGeneratorTests : IClassFixture<JwtTokenGeneratorFixture>
    { 
        private readonly JwtTokenGeneratorFixture _fixture;

        public JwtTokenGeneratorTests(JwtTokenGeneratorFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Decode_Token()
        {
            var token = _fixture.JwtTokenGenerator.DecodeToken(_fixture.Token);
            token.Should().BeOfType<JwtSecurityToken>();
        }

        [Fact]
        public void Get_UserObjectId_From_Token()
        {
            var objectId = _fixture.JwtTokenGenerator.GetUserObjectIdFromToken(_fixture.Token);
            objectId.Should().BeEquivalentTo("34812bd6-c8f1-44f1-b137-2d37246110da");
        }

        [Fact]
        public void Get_AzureADUser_From_Token()
        {
            var user = _fixture.JwtTokenGenerator.GetAzureADUserFromToken(_fixture.Token);
            user.Should().NotBeNull();
        }
    }
}