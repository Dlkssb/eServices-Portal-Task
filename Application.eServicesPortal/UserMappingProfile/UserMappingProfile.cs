using Application.eServicesPortal.DTOs;
using AutoMapper;
using Domain.eServicesPortal.Entities;

namespace Application.eServicesPortal.UserMappingProfile
{
    public class UserMappingProfile:Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
