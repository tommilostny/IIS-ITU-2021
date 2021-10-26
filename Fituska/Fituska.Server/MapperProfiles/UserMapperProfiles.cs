namespace Fituska.Server.MapperProfiles;

public class UserMapperProfiles : Profile
{
    public UserMapperProfiles()
    {
        CreateMap<UserEntity, UserDetailModel>();

        CreateMap<UserEntity, UserListModel>();

        CreateMap<UserRegistrationModel, UserEntity>()
            .ForMember(dst => dst.UserName, config => config.MapFrom<UsernameResolver>());
    }

    private class UsernameResolver : IValueResolver<UserRegistrationModel, UserEntity, string>
    {
        public string Resolve(UserRegistrationModel source, UserEntity destination, string destMember, ResolutionContext context)
        {
            return source.Email;
        }
    }
}
