using Fituska.Shared.Models.File;

namespace Fituska.BL.MapperProfiles;

public class FileMapperProfiles : Profile
{
    public FileMapperProfiles()
    {
        CreateMap<FileEntity, FileAnswerModel>();
        CreateMap<FileEntity, FileCommentModel>();
        CreateMap<FileEntity, FileQuestionModel>();
        
        CreateMap<FileEntity, FileListModel>();
        CreateMap<FileEntity, FileModelBase>();

        CreateMap<FileAnswerModel, FileEntity>()
            .ForMember(dst => dst.Answer, config => config.Ignore())
            .ForMember(dst => dst.Comment, config => config.Ignore())
            .ForMember(dst => dst.Question, config => config.Ignore())
            .ForMember(dst => dst.CommentId, config => config.Ignore())
            .ForMember(dst => dst.QuestionId, config => config.Ignore());
    
        CreateMap<FileCommentModel, FileEntity>()
            .ForMember(dst => dst.Answer, config => config.Ignore())
            .ForMember(dst => dst.Comment, config => config.Ignore())
            .ForMember(dst => dst.Question, config => config.Ignore())
            .ForMember(dst => dst.AnswerId, config => config.Ignore())
            .ForMember(dst => dst.QuestionId, config => config.Ignore());

        CreateMap<FileQuestionModel, FileEntity>()
            .ForMember(dst => dst.Answer, config => config.Ignore())
            .ForMember(dst => dst.Comment, config => config.Ignore())
            .ForMember(dst => dst.Question, config => config.Ignore())
            .ForMember(dst => dst.AnswerId, config => config.Ignore())
            .ForMember(dst => dst.CommentId, config => config.Ignore());

        CreateMap<UserEntity, FileUserModel>()
            .ForMember(dst => dst.Content, config => config.MapFrom(src => src.Photo))
            .ForMember(dst => dst.Name, config => config.MapFrom(src => src.PhotoFileName))
            .ForMember(dst => dst.UserId, config => config.MapFrom(src => src.Id))
            .ForMember(dst => dst.Id, config => config.Ignore());
    }
}
