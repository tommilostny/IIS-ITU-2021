using Fituska.Shared.Models.Category;

namespace Fituska.BL.MapperProfiles;
public class CategoryMapperProfiles:Profile
{
    public CategoryMapperProfiles()
    {
        CreateMap<CategoryEntity,CategoryDetailModel>();
        CreateMap<CategoryEntity,CategoryListModel>();
    }
}
