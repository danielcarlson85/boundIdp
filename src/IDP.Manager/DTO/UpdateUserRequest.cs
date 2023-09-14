using Microsoft.Graph;

namespace Bound.IDP.Managers.DTO
{
    public class UpdateUserRequest : UserDTOBase
    {
        public PasswordProfile PasswordProfile { get; set; }
    }
}