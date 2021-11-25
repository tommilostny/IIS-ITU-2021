using Fituska.Shared.Models.Comment;

namespace Fituska.BL.MapperProfiles;

public class CommentMapperProfiles : Profile
{
    public CommentMapperProfiles()
    {
        CreateMap<CommentEntity, CommentDetailModel>();

        CreateMap<CommentNewModel, CommentEntity>()
            .ForMember(dst => dst.Answer, config => config.Ignore())
            .ForMember(dst => dst.Files, config => config.Ignore())
            .ForMember(dst => dst.ParentComment, config => config.Ignore())
            .ForMember(dst => dst.SubComments, config => config.Ignore())
            .ForMember(dst => dst.User, config => config.Ignore());
    }
}
