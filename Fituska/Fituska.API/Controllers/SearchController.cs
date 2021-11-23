using Fituska.BL.Repositories;
using Fituska.Shared.Models.Search;
using Microsoft.AspNetCore.Identity;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly AnswerRepository _answerRepository;
    private readonly CourseRepository _courseRepository;
    private readonly QuestionRepository _questionRepository;
    private readonly UserManager<UserEntity> _userManager;
    private readonly IMapper _mapper;

    public SearchController(
        AnswerRepository answerRepository,
        CourseRepository courseRepository,
        QuestionRepository questionRepository,
        UserManager<UserEntity> userManager,
        IMapper mapper)
    {
        _answerRepository = answerRepository;
        _courseRepository = courseRepository;
        _questionRepository = questionRepository;
        _userManager = userManager;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult Search([FromBody] SearchRequestModel searchRequest)
    {
        var term = searchRequest.SearchTerm;
        if (string.IsNullOrEmpty(term))
            return BadRequest();

        var response = new SearchResponseModel();

        if (searchRequest.IncludeAnswers)
            response.Answers = _mapper.Map<List<SearchAnswerModel>>(_answerRepository.Search(term));

        if (searchRequest.IncludeQuestions)
            response.Questions = _mapper.Map<List<SearchQuestionModel>>(_questionRepository.Search(term));

        if (searchRequest.IncludeCourses)
            response.Courses = _mapper.Map<List<SearchCourseModel>>(_courseRepository.Search(term));

        if (searchRequest.IncludeUsers)
            response.Users = _mapper.Map<List<SearchUserModel>>(_userManager.Users
                .Where(u => u.UserName.Contains(term)
                    || (u.FirstName != null && u.FirstName.Contains(term))
                    || (u.LastName != null && u.LastName.Contains(term)))
                .ToList());

        return Ok(response);
    }
}
