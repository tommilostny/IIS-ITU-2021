using Fituska.Shared.Models.User;

namespace Fituska.Shared.Models.CourseAttendance;

public record CourseAttendanceListModel : ModelBase
{
    public short AttendingYear { get; set; }
    public UserListModel User { get; set; }
}
