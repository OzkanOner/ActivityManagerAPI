using AutoMapper;
using ActivityManagerAPI.Models;
using ActivityManagerAPI.Models.Dtos;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Activity, ActivityDTO>()
            .ForMember(dest => dest.CreatedUser, opt => opt.MapFrom(src => src.CreatedUser));

        CreateMap<User, UserDTO>();
        CreateMap<UserActivity, UserActivityDTO>();
    }
}
