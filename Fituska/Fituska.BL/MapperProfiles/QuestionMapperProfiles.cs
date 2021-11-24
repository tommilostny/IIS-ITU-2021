using Fituska.Shared.Models.Question;

namespace Fituska.BL.MapperProfiles;
public class QuestionMapperProfiles : Profile
{
    public QuestionMapperProfiles()
    {
        CreateMap<QuestionEntity, QuestionDetailModel>();
        CreateMap<QuestionEntity, QuestionListModel>();
    }
}
