using Fituska.Shared.Models.Category;

namespace Fituska.BL.MapperProfiles;

public class CategoryMapperProfiles : Profile
{
    public CategoryMapperProfiles()
    {
        CreateMap<CategoryEntity, CategoryListModel>();

        CreateMap<CategoryEntity, CategoryDetailModel>();

        CreateMap<CategoryNewModel, CategoryEntity>()
            .ForMember(dst => dst.Questions, config => config.Ignore())
            .ForMember(dst => dst.Course, config => config.Ignore());
    }
}
