using Fituska.Shared.Models.User;

namespace Fituska.BL.MapperProfiles;

public class UserMapperProfiles : Profile
{
    public UserMapperProfiles()
    {
        CreateMap<UserEntity, UserDetailModel>()
            .ForMember(dst => dst.RegistrationDate, config => config.MapFrom(src => src.RegistrationDate.ToLocalTime()))
            .ForMember(dst => dst.LastLoginDate, config => config.MapFrom<LastLoginUtcToLocalTimeResolver>());

        CreateMap<UserEntity, UserListModel>();

        CreateMap<UserRegistrationModel, UserEntity>()
            .ForMember(dst => dst.AttendingCourses, config => config.Ignore())
            .ForMember(dst => dst.RegistrationDate, config => config.MapFrom(_ => DateTime.UtcNow));
    }

    private class LastLoginUtcToLocalTimeResolver : IValueResolver<UserEntity, UserDetailModel, DateTime?>
    {
        public DateTime? Resolve(UserEntity source, UserDetailModel destination, DateTime? destMember, ResolutionContext context)
        {
            return source.LastLoginDate is not null ? source.LastLoginDate.Value.ToLocalTime() : DateTime.Now;
        }
    }
}
