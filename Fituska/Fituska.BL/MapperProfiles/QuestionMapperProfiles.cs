using Fituska.Shared.Models.Question;

namespace Fituska.BL.MapperProfiles;

public class QuestionMapperProfiles : Profile
{
    public QuestionMapperProfiles()
    {
        CreateMap<UserSawQuestionEntity, UserSawQuestionModel>();

        CreateMap<QuestionEntity, QuestionDetailModel>()
            .ForMember(dst => dst.CreationTime, config => config.MapFrom(src => src.CreationTime.ToLocalTime()));

        CreateMap<QuestionEntity, QuestionListModel>()
            .ForMember(dst => dst.CreationTime, config => config.MapFrom(src => src.CreationTime.ToLocalTime()));

        CreateMap<UserSawQuestionModel, UserSawQuestionEntity>()
            .ForMember(dst => dst.Question, config => config.Ignore())
            .ForMember(dst => dst.User, config => config.Ignore());

        CreateMap<QuestionNewModel, QuestionEntity>()
            .ForMember(dst => dst.User, config => config.Ignore())
            .ForMember(dst => dst.Category, config => config.Ignore())
            .ForMember(dst => dst.Answers, config => config.Ignore())
            .ForMember(dst => dst.UserSawQuestions, config => config.Ignore())
            .ForMember(dst => dst.Files, config => config.Ignore())
            .ForMember(dst => dst.CreationTime, config => config.MapFrom(_ => DateTime.UtcNow));
    }
}
