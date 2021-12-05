using Fituska.Shared.Models.Answer;

namespace Fituska.BL.MapperProfiles;

public class AnswerMapperProfiles : Profile
{
    public AnswerMapperProfiles()
    {
        CreateMap<AnswerDetailModel, AnswerEntity>();
        CreateMap<AnswerEntity, AnswerDetailModel>()
            .ForMember(dst => dst.CreationTime, config => config.MapFrom(src => src.CreationTime.ToLocalTime()))
            .ForMember(dst => dst.ModifiedTime, config => config.MapFrom<ModifiedTimeUtcToLocalTimeResolver>());

        CreateMap<AnswerNewModel, AnswerEntity>()
            .ForMember(dst => dst.Question, config => config.Ignore())
            .ForMember(dst => dst.User, config => config.Ignore())
            .ForMember(dst => dst.Votes, config => config.Ignore())
            .ForMember(dst => dst.Comments, config => config.Ignore())
            .ForMember(dst => dst.Files, config => config.Ignore())
            .ForMember(dst => dst.CreationTime, config => config.MapFrom(_ => DateTime.UtcNow));
    }

    private class ModifiedTimeUtcToLocalTimeResolver : IValueResolver<AnswerEntity, AnswerDetailModel, DateTime?>
    {
        public DateTime? Resolve(AnswerEntity source, AnswerDetailModel destination, DateTime? destMember, ResolutionContext context)
        {
            if (source.ModifiedTime.HasValue)
            {
                return source.ModifiedTime.Value.ToLocalTime();
            }
            return null;
        }
    }
}
