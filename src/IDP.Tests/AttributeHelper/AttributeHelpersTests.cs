using Bound.IDP.Managers.Helpers;
using FluentAssertions;
using Xunit;

namespace Bound.IDP.Tests.AttributeHelper
{
    public class AttributeHelpersTests : IClassFixture<AttributeHelpersFixture>
    {
        private readonly AttributeHelpersFixture _fixture;
        public AttributeHelpersTests(AttributeHelpersFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Create_New_Attribute()
        {
            var userWithAttribute = AttributeHelpers.CreateNewAttribute(_fixture.User, "bc2cTestid", "attrNameTest", "attrvaluetest");
            userWithAttribute.AdditionalData.Should().NotBeEmpty();
        }

        [Fact]
        public void Get_Custom_Attribute()
        {
            string fullAttrubuteName = AttributeHelpers.GetCustomAttribute("attrNameTest", "bc2cTestid");
            fullAttrubuteName.Should().Be("extension_bc2cTestid_attrNameTest");
        }
    }
}