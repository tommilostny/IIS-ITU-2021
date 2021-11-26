namespace Fituska.Shared.Models.Search;

public class SearchResponseModel
{
    public List<SearchAnswerModel>? Answers { get; set; }
    public List<SearchCourseModel>? Courses { get; set; }
    public List<SearchQuestionModel>? Questions { get; set; }
    public List<SearchUserModel>? Users { get; set; }
}
