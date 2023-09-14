namespace Bound.IDP.Abstractions.Models.AzureADB2C.Tokens
{
    public class ADUserTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string id_token { get; set; }
    }
}
