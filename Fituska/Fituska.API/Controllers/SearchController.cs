using Fituska.BL.Repositories;
using Fituska.Shared.Models.Search;
using Microsoft.AspNetCore.Identity;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly AnswerRepository answerRepository;
    private readonly CourseRepository courseRepository;
    private readonly QuestionRepository questionRepository;
    private readonly UserManager<UserEntity> userManager;
    private readonly IMapper mapper;

    public SearchController(
        AnswerRepository answerRepository,
        CourseRepository courseRepository,
        QuestionRepository questionRepository,
        UserManager<UserEntity> userManager,
        IMapper mapper)
    {
        this.answerRepository = answerRepository;
        this.courseRepository = courseRepository;
        this.questionRepository = questionRepository;
        this.userManager = userManager;
        this.mapper = mapper;
    }

    [HttpPost]
    public IActionResult Search([FromBody] SearchRequestModel searchRequest)
    {
        var term = searchRequest.SearchTerm;
        if (string.IsNullOrEmpty(term))
            return BadRequest();

        var response = new SearchResponseModel();

        if (searchRequest.IncludeAnswers)
            response.Answers = mapper.Map<List<SearchAnswerModel>>(answerRepository.Search(term));

        if (searchRequest.IncludeQuestions)
            response.Questions = mapper.Map<List<SearchQuestionModel>>(questionRepository.Search(term));

        if (searchRequest.IncludeCourses)
            response.Courses = mapper.Map<List<SearchCourseModel>>(courseRepository.Search(term));

        if (searchRequest.IncludeUsers)
            response.Users = mapper.Map<List<SearchUserModel>>(userManager.Users
                .Where(u => u.UserName.Contains(term)
                    || (u.FirstName != null && u.FirstName.Contains(term))
                    || (u.LastName != null && u.LastName.Contains(term)))
                .ToList());

        return Ok(response);
    }
}
