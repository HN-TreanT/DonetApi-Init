using AutoMapper;
using DotnetApi.CoreLib.QuanLyUser.DTO;
using DotnetApi.CoreLib.Users.Entities;

namespace DotnetApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserGet>();
        }
    }
}