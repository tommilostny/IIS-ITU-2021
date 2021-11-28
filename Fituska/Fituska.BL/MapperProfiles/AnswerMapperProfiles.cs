using Fituska.Shared.Models.Answer;

namespace Fituska.BL.MapperProfiles;

public class AnswerMapperProfiles : Profile
{
    public AnswerMapperProfiles()
    {
        CreateMap<UserSawAnswerEntity, UserSawAnswerModel>();
        CreateMap<AnswerDetailModel, AnswerEntity>();
        CreateMap<AnswerEntity, AnswerDetailModel>()
            .ForMember(dst => dst.CreationTime, config => config.MapFrom(src => src.CreationTime.ToLocalTime()));

        CreateMap<UserSawAnswerModel, UserSawAnswerEntity>()
            .ForMember(dst => dst.Answer, config => config.Ignore());
        
        CreateMap<AnswerNewModel, AnswerEntity>()
            .ForMember(dst => dst.Question, config => config.Ignore())
            .ForMember(dst => dst.User, config => config.Ignore())
            .ForMember(dst => dst.UsersSawAnswer, config => config.Ignore())
            .ForMember(dst => dst.UsersVoteAnswers, config => config.Ignore())
            .ForMember(dst => dst.Comments, config => config.Ignore())
            .ForMember(dst => dst.Files, config => config.Ignore())
            .ForMember(dst => dst.CreationTime, config => config.MapFrom(_ => DateTime.UtcNow));
    }
}
