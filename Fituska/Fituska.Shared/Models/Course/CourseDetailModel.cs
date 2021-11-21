﻿using Fituska.Shared.Enums;
using Fituska.Shared.Models.Category;

namespace Fituska.Shared.Models.Course;

public record CourseDetailModel : ModelBase
{
    public short AcademicYear { get; set; }
    public string Name { get; set; }
    public string Shortcut { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public byte Credits { get; set; }
    public YearOfStudy YearOfStudy { get; set; }
    public Semester Semester { get; set; }
    public List<CategoryModel> Categories { get; set; }
}
