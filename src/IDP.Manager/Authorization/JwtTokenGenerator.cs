using Bound.IDP.Abstractions.Constants;
using Bound.IDP.Abstractions.Interfaces;
using Bound.IDP.Abstractions.Models.AzureADB2C.User;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Bound.IDP.Managers.Authorization
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public JwtTokenGenerator(JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }
        /// <summary>
        /// Decodes a base64 encoded jwt token.
        /// </summary>
        /// <param name="token">The base 64 encoded token</param>
        public ADUserResponse GetAzureADUserFromToken(string token)
        {
            var claims = _jwtSecurityTokenHandler.ReadJwtToken(token).Claims.ToList();

            var userTokenObject = new ADUserResponse
            {
                oid = claims.Any(oid => oid.Type.Equals(ADConstants.ADUserResponse.Oid)) ? claims.Find(oid => oid.Type.Equals(ADConstants.ADUserResponse.Oid)).Value : string.Empty,
                exp = claims.Any(oid => oid.Type.Equals(ADConstants.ADUserResponse.Exp)) ? claims.Find(oid => oid.Type.Equals(ADConstants.ADUserResponse.Exp)).Value : string.Empty,
                aud = claims.Any(oid => oid.Type.Equals(ADConstants.ADUserResponse.Aud)) ? claims.Find(oid => oid.Type.Equals(ADConstants.ADUserResponse.Aud)).Value : string.Empty,
                given_name = claims.Any(oid => oid.Type.Equals(ADConstants.ADUserResponse.GivenName)) ? claims.Find(oid => oid.Type.Equals(ADConstants.ADUserResponse.GivenName)).Value : string.Empty,
                family_name = claims.Any(oid => oid.Type.Equals(ADConstants.ADUserResponse.FamilyName)) ? claims.Find(oid => oid.Type.Equals(ADConstants.ADUserResponse.FamilyName)).Value : string.Empty,
                city = claims.Any(oid => oid.Type.Equals(ADConstants.ADUserResponse.City)) ? claims.Find(oid => oid.Type.Equals(ADConstants.ADUserResponse.City)).Value : string.Empty,
                country = claims.Any(oid => oid.Type.Equals(ADConstants.ADUserResponse.Country)) ? claims.Find(oid => oid.Type.Equals(ADConstants.ADUserResponse.Country)).Value : string.Empty,
                postalCode = claims.Any(oid => oid.Type.Equals(ADConstants.ADUserResponse.PostalCode)) ? claims.Find(oid => oid.Type.Equals(ADConstants.ADUserResponse.PostalCode)).Value : string.Empty,
                state = claims.Any(oid => oid.Type.Equals(ADConstants.ADUserResponse.State)) ? claims.Find(oid => oid.Type.Equals(ADConstants.ADUserResponse.State)).Value : string.Empty,
                streetAddress = claims.Any(oid => oid.Type.Equals(ADConstants.ADUserResponse.StreetAddress)) ? claims.Find(oid => oid.Type.Equals(ADConstants.ADUserResponse.StreetAddress)).Value : string.Empty,
                email = claims.Any(oid => oid.Type.Equals(ADConstants.ADUserResponse.Emails)) ? claims.Find(oid => oid.Type.Equals(ADConstants.ADUserResponse.Emails)).Value : string.Empty,
                role = claims.Any(oid => oid.Type.Equals(ADConstants.ADUserResponse.Role)) ? claims.Find(oid => oid.Type.Equals(ADConstants.ADUserResponse.Role)).Value : "User",
            };

            return userTokenObject;
        }

        /// <param name="token">The base 64 encoded token</param>
        public string GetUserObjectIdFromToken(string token)
        {
            var tokenString = _jwtSecurityTokenHandler.ReadJwtToken(token);
            var objectId = tokenString.Claims.Where(oid => oid.Type.Equals(ADConstants.ADUserResponse.Oid)).FirstOrDefault().Value;
            return objectId;
        }

        /// <summary>
        /// Decodes a base64 encoded jwt token.
        /// </summary>
        /// <param name="token">The base 64 encoded token</param>
        public JwtSecurityToken DecodeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(token);
        }
    }
}