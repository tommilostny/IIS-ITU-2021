using Fituska.Shared.Enums;

namespace Fituska.Client.Services;

public static class ConverterService
{
    public static string YearOfStudyDecode(YearOfStudy yearOfStudy)
    {
        return yearOfStudy switch
        {
            YearOfStudy.BIT1 => "1BIT",
            YearOfStudy.BIT2 => "2BIT",
            YearOfStudy.BIT3 => "3BIT",
            YearOfStudy.MIT1 => "1MIT",
            YearOfStudy.MIT2 => "2MIT",
            _ => throw new NotImplementedException()
        };
    }

    public static string SemesterDecode(Semester semester)
    {
        return semester switch
        {
            Semester.Summer => "letní",
            Semester.Winter => "zimní",
            _ => throw new NotImplementedException()
        };
    }
}
