using Fituska.Shared.Models.Vote;

namespace Fituska.BL.MapperProfiles;

public class VoteMapperProfiles : Profile
{
    public VoteMapperProfiles()
    {
        CreateMap<VoteEntity, VoteModel>();

        CreateMap<VoteModel, VoteEntity>()
            .ForMember(dst => dst.Answer, config => config.Ignore())
            .ForMember(dst => dst.User, config => config.Ignore())
            .ForMember(dst => dst.UserId, config => config.MapFrom(src => src.User.Id));
    }
}
