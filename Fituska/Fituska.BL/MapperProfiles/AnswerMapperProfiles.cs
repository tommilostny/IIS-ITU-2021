using Fituska.Shared.Models.Answer;

namespace Fituska.BL.MapperProfiles;
public class AnswerMapperProfiles : Profile
{
    public AnswerMapperProfiles()
    {
        CreateMap<AnswerEntity,AnswerDetailModel>();
        CreateMap<AnswerEntity,AnswerListModel>();
    }
}
