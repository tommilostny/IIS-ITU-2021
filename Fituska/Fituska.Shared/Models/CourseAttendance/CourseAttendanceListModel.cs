using Fituska.Shared.Models.Course;
using Fituska.Shared.Models.User;

namespace Fituska.Shared.Models.CourseAttendance;

public class CourseAttendanceListModel
{
    public short AttendingYear { get; set; }
    public UserListModel User { get; set; }
    public CourseListModel Course { get; set; }
}
