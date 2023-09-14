using Bound.IDP.Abstractions.Models.AzureADB2C.User;
using Bound.IDP.Managers.DTO;
using Microsoft.Graph;

namespace Bound.IDP.Managers.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<User, GetUserResponse>();
            CreateMap<GetUserResponse, User>();

            CreateMap<CreateUserRequest, User>();
            CreateMap<User, CreateUserRequest>();

            CreateMap<User, CreateUserResponse>();

            CreateMap<UpdateUserRequest, User>();
                //.ForMember(dest => dest.PasswordProfile.Password, opt => opt.MapFrom(src => src.PasswordProfile))
                //.ForMember(dest => dest.GivenName, opt => opt.MapFrom(src => src.GivenName))
                //.ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                //.ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                //.ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                //.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                //.ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                //.ForMember(dest => dest.StreetAddress, opt => opt.MapFrom(src => src.StreetAddress))
                //.ForMember(dest => dest.MobilePhone, opt => opt.MapFrom(src => src.MobilePhone));

            CreateMap<User, UpdateUserRequest>();

            CreateMap<User, UpdateUserResponse>();

            CreateMap<CreateUserRequest, User>()
            .ForMember(dest => dest.Mail, opt => opt.MapFrom(src => src.Email));


            CreateMap<ADUserResponse, CleanADUserResponse>()
            .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.access_token))
            .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.refresh_token))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.family_name))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.given_name))
            .ForMember(dest => dest.ObjectId, opt => opt.MapFrom(src => src.oid))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.role));
        }
    }
}