using Microsoft.Graph;
using System.Collections.Generic;

namespace Bound.IDP.Managers.Helpers
{
    public class UserHelpers
    {
        public static User AddAdditionalSettings(User user, string password, string tenantId, string graphB2cExtensionAppClientId, string role)
        {
            user.PasswordProfile = new PasswordProfile()
            {
                ForceChangePasswordNextSignIn = false,
                Password = password,
                ODataType = null
            };

            user.Identities = new List<ObjectIdentity>() { new ObjectIdentity()
                {
                    SignInType = "emailAddress",
                    IssuerAssignedId = user.Mail,
                    Issuer = tenantId,
                }
            };

            user.DisplayName = user.Mail;

            user = AttributeHelpers.CreateNewAttribute(user, graphB2cExtensionAppClientId, "Role", role);

            return user;
        }
    }
}
