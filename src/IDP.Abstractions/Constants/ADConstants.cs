namespace Bound.IDP.Abstractions.Constants
{
    public class ADConstants
    {
        public class ADUserRequest
        {
            public const string Username = "username";
            public const string Password = "password";
            public const string GrantType = "grant_type";
            public const string Scope = "scope";
            public const string ClientId = "client_id";
            public const string ResponseType = "response_type";
            public const string ContentType = "contenttype";
            public const string BaseAddress = "baseaddress";
        }

        public class ADRefreshTokenRequest
        {
            public const string GrantType = "grant_type";
            public const string ResponseType = "response_type";
            public const string ClientId = "client_id";
            public const string Resource = "resource";
            public const string RefreshToken = "refresh_token";
            public const string IdToken = "id_token";


        }

        public class ADUserResponse
        {
            public const string Iss = "iss";
            public const string Exp = "exp";
            public const string Nbf = "nbf";
            public const string Aud = "aud";
            public const string Oid = "oid";
            public const string Idp = "idp";
            public const string Sub = "sub";
            public const string GivenName = "given_name";
            public const string FamilyName = "family_name";
            public const string Name = "name";
            public const string NewUser = "newUser";
            public const string City = "city";
            public const string Country = "country";
            public const string JobTitle = "jobTitle";
            public const string PostalCode = "postalCode";
            public const string State = "state";
            public const string StreetAddress = "streetAddress";
            public const string Tfp = "tfp";
            public const string Azp = "azp";
            public const string Ver = "ver";
            public const string Iat = "iat";
            public const string Emails = "emails";
            public const string Role = "extension_Role";
        }
    }
}