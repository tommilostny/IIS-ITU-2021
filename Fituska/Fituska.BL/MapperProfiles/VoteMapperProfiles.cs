using Fituska.Shared.Models.Vote;

namespace Fituska.BL.MapperProfiles;
public class VoteMapperProfiles : Profile
{
    public VoteMapperProfiles()
    {
        CreateMap<VoteEntity,VoteDetailModel>();
        CreateMap<VoteEntity,VoteListModel>();
    }
}

