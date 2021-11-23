using Fituska.Shared.Models.Search;

namespace Fituska.BL.MapperProfiles;

public class SearchMapperProfiles : Profile
{
    public SearchMapperProfiles()
    {
        CreateMap<AnswerEntity, SearchAnswerModel>();
        CreateMap<QuestionEntity, SearchQuestionModel>();
        CreateMap<CourseEntity, SearchCourseModel>();
        CreateMap<UserEntity, SearchUserModel>();
    }
}
