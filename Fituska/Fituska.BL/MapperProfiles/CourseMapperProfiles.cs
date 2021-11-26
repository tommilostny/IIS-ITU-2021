using Fituska.Shared.Models.Course;

namespace Fituska.BL.MapperProfiles;

public class CourseMapperProfiles : Profile
{
    public CourseMapperProfiles()
    {
        CreateMap<CourseEntity, CourseDetailModel>();

        CreateMap<CourseEntity, CourseListModel>();

        CreateMap<CourseNewModel, CourseEntity>()
            .ForMember(dst => dst.Attendees, config => config.Ignore())
            .ForMember(dst => dst.Categories, config => config.Ignore())
            .ForMember(dst => dst.Lecturer, config => config.Ignore())
            .ForMember(dst => dst.Url, config => config.MapFrom<CourseUrlResolver>())
            .ForMember(dst => dst.ModeratorApproved, config => config.MapFrom(_ => false));
    }

    private class CourseUrlResolver : IValueResolver<CourseNewModel, CourseEntity, string>
    {
        public string Resolve(CourseNewModel source, CourseEntity destination, string destMember, ResolutionContext context)
        {
            return $"{source.Semester.ToString().ToLower()}-{source.YearOfStudy.ToString().ToLower()}-{source.Shortcut}";
        }
    }
}
