using Microsoft.Graph;
using NSubstitute;

namespace Bound.IDP.Tests.AttributeHelper
{
    public class AttributeHelpersFixture
    {
        public User User { get; set; }

        public AttributeHelpersFixture()
        {
            User = Substitute.For<User>();
        }
    }
}
