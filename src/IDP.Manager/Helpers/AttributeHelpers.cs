using Microsoft.Graph;
using System.Collections.Generic;

namespace Bound.IDP.Managers.Helpers
{
    public class AttributeHelpers
    {
        public static User CreateNewAttribute(User user, string b2cExtensionAppClientId, string attributeName, string attributeValue)
        {

            //This is to add extra attribute to User
            //Make sure the attribute is added in Azure ADB2C

            IDictionary<string, object> extensionInstance = new Dictionary<string, object>();
            string myOwnPropertieExtension = GetCustomAttribute(attributeName, b2cExtensionAppClientId);
            extensionInstance.Add(myOwnPropertieExtension, attributeValue);
            user.AdditionalData = extensionInstance;

            return user;
        }

        public static string GetCustomAttribute(string attributeName, string b2cExtensionAppClientId)
        {
            b2cExtensionAppClientId = b2cExtensionAppClientId.Replace("-", "");
            string myOwnPropertieExtension = $"extension_{b2cExtensionAppClientId}_{attributeName}";
            return myOwnPropertieExtension;
        }
    }
}



//This is to add extra properties to Azure AD B2C User
//IDictionary<string, object> extensionInstance = new Dictionary<string, object>();
//string myOwnPropertieExtension = $"extension_{Config.AppConfig.TenantId}_MyOwnPropertie";
//string myOwnPropertiesExtension = $"extension_{Config.AppConfig.TenantId}_MyOwnProperties";
//extensionInstance.Add(myOwnPropertieExtension, "valueHere");
//extensionInstance.Add(myOwnPropertiesExtension, true);