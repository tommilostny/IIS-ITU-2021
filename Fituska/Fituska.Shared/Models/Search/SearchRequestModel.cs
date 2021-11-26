namespace Fituska.Shared.Models.Search;

public class SearchRequestModel
{
    public bool IncludeAnswers { get; set; } = true;
    public bool IncludeCourses { get; set; } = true;
    public bool IncludeQuestions { get; set; } = true;
    public bool IncludeUsers { get; set; } = true;
    public string SearchTerm { get; set; } = string.Empty;
}
