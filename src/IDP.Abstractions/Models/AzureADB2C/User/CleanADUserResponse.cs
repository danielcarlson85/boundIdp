namespace Bound.IDP.Abstractions.Models.AzureADB2C.User
{
    public class CleanADUserResponse
    {
        public string ObjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public string Role { get; set; }
    }
}
