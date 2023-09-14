namespace Bound.IDP.Abstractions.Models.Settings
{
    public class ADAuthSettings
    {
        public string GrantType { get; set; }
        public string Scope { get; set; }
        public string ClientId { get; set; }
        public string ResponseType { get; set; }
        public string ContentType { get; set; }
        public string BaseAddress { get; set; }
    }
}