using Fituska.Shared.Models.File;

namespace Fituska.BL.MapperProfiles;

public class FileMapperProfiles : Profile
{
    public FileMapperProfiles()
    {
        CreateMap<FileEntity, FileAnswerModel>();
        CreateMap<FileEntity, FileCommentModel>();
        CreateMap<FileEntity, FileQuestionModel>();

        CreateMap<FileAnswerModel, FileEntity>()
            .ForMember(dst => dst.Answer, config => config.Ignore())
            .ForMember(dst => dst.Comment, config => config.Ignore())
            .ForMember(dst => dst.Question, config => config.Ignore());

        CreateMap<FileCommentModel, FileEntity>()
            .ForMember(dst => dst.Answer, config => config.Ignore())
            .ForMember(dst => dst.Comment, config => config.Ignore())
            .ForMember(dst => dst.Question, config => config.Ignore());

        CreateMap<FileQuestionModel, FileEntity>()
            .ForMember(dst => dst.Answer, config => config.Ignore())
            .ForMember(dst => dst.Comment, config => config.Ignore())
            .ForMember(dst => dst.Question, config => config.Ignore());
    }
}
