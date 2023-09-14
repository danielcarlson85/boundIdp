namespace Bound.IDP.Abstractions.Models.AzureADB2C.User
{
    public class ADUserResponse
    {
        public string iss { get; set; }
        public string exp { get; set; }
        public string nbf { get; set; }
        public string aud { get; set; }
        public string idp { get; set; }
        public string oid { get; set; }
        public string sub { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string name { get; set; }
        public string newUser { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string jobTitle { get; set; }
        public string postalCode { get; set; }
        public string state { get; set; }
        public string streetAddress { get; set; }
        public string tfp { get; set; }
        public string azp { get; set; }
        public string ver { get; set; }
        public string iat { get; set; }
        public string refresh_token { get; set; }
        public string access_token { get; set; }
        public string email { get; set; }
        public string role { get; set; }
    }
}
