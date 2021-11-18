using Fituska.Shared.Models.User;

namespace Fituska.BL.MapperProfiles;

public class UserMapperProfiles : Profile
{
    public UserMapperProfiles()
    {
        CreateMap<UserEntity, UserDetailModel>()
            .ForMember(dst => dst.RegistrationDate, config => config.MapFrom<RegistrationUtcToLocalTimeResolver>())
            .ForMember(dst => dst.LastLoginDate, config => config.MapFrom<LastLoginUtcToLocalTimeResolver>());

        CreateMap<UserEntity, UserListModel>();

        CreateMap<UserRegistrationModel, UserEntity>()
            .ForMember(dst => dst.RegistrationDate, config => config.MapFrom<RegistrationDateResolver>());
    }

    private class RegistrationDateResolver : IValueResolver<UserRegistrationModel, UserEntity, DateTime>
    {
        public DateTime Resolve(UserRegistrationModel source, UserEntity destination, DateTime destMember, ResolutionContext context)
        {
            return DateTime.UtcNow;
        }
    }

    private class RegistrationUtcToLocalTimeResolver : IValueResolver<UserEntity, UserDetailModel, DateTime>
    {
        public DateTime Resolve(UserEntity source, UserDetailModel destination, DateTime destMember, ResolutionContext context)
        {
            return source.RegistrationDate.ToLocalTime();
        }
    }

    private class LastLoginUtcToLocalTimeResolver : IValueResolver<UserEntity, UserDetailModel, DateTime?>
    {
        public DateTime? Resolve(UserEntity source, UserDetailModel destination, DateTime? destMember, ResolutionContext context)
        {
            if (source.LastLoginDate.HasValue)
            {
                return source.LastLoginDate.Value.ToLocalTime();
            }
            return DateTime.Now;
        }
    }
}
