using Fituska.Shared.Models.User;
using Fituska.Shared.Models.Course;

namespace Fituska.Shared.Models.CourseAttendance;

public record CourseAttendanceListModel : ModelBase
{
    public short AttendingYear { get; set; }
    public UserListModel User { get; set; }
    public CourseListModel Course { get; set; }
}
