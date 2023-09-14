namespace Bound.IDP.Abstractions.Models.Settings
{
    public class ADGraphSettings
    {
        public string TenantId { get; set; }
        public string GraphAppId { get; set; }
        public string GraphClientSecret { get; set; }
        public string GraphB2cExtensionAppClientId { get; set; }
    }
}