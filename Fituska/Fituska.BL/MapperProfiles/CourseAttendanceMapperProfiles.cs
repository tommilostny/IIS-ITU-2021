using Fituska.Shared.Models.CourseAttendance;

namespace Fituska.BL.MapperProfiles;

public class CourseAttendanceMapperProfiles : Profile
{
    public CourseAttendanceMapperProfiles()
    {
        CreateMap<CourseAttendanceEntity, CourseAttendanceListModel>();

        CreateMap<CourseAttendanceNewModel, CourseAttendanceEntity>()
            .ForMember(dst => dst.Course, config => config.Ignore())
            .ForMember(dst => dst.User, config => config.Ignore());
    }
}
