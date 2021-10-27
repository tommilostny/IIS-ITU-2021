using Fituska.Shared.Models.User;

namespace Fituska.BL.MapperProfiles;

public class UserMapperProfiles : Profile
{
    public UserMapperProfiles()
    {
        CreateMap<UserEntity, UserDetailModel>();

        CreateMap<UserEntity, UserListModel>();

        CreateMap<UserRegistrationModel, UserEntity>()
            .ForMember(dst => dst.UserName, config => config.MapFrom<UsernameResolver>())
            .ForMember(dst => dst.RegistrationDate, config => config.MapFrom<RegistrationDateResolver>());
    }

    private class UsernameResolver : IValueResolver<UserRegistrationModel, UserEntity, string>
    {
        public string Resolve(UserRegistrationModel source, UserEntity destination, string destMember, ResolutionContext context)
        {
            return source.Email;
        }
    }

    private class RegistrationDateResolver : IValueResolver<UserRegistrationModel, UserEntity, DateTime>
    {
        public DateTime Resolve(UserRegistrationModel source, UserEntity destination, DateTime destMember, ResolutionContext context)
        {
            return DateTime.UtcNow;
        }
    }
}
