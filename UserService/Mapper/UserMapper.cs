using AutoMapper;
using UserService.DTO;
using UserService.Models;

namespace UserService.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<Users, UserReadDTO>();
            CreateMap<UserCreateDTO, Users>();
        }
    }
}
