using Fituska.Shared.Models.Course;
using Fituska.Shared.Models.User;

namespace Fituska.Shared.Models.CourseAttendcy;
public class CourseAttendencyDetailModel
{
    public short AttendingYear { get; set; }
    public Guid UserId { get; set; }
    public UserDetailModel? User { get; set; }
    public Guid CourseId { get; set; }
    public CourseDetailModel? Course { get; set; }
}
