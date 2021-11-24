using Fituska.Shared.Models.Discussion;

namespace Fituska.BL.MapperProfiles;
public class DiscussionMapperProfiles : Profile
{
    public DiscussionMapperProfiles()
    {
        CreateMap<DiscussionEntity,DiscussionDetailModel>();
        CreateMap<DiscussionEntity,DiscussionListModel>();
    }
}
