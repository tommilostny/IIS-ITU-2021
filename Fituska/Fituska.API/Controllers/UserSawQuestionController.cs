using Fituska.BL.Repositories;
using Fituska.Shared.Models.Question;
using NSwag.Annotations;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class UserSawQuestionController : ControllerBase
{
    private readonly UserSawQuestionRepository repository;
    private readonly IMapper mapper;
    public UserSawQuestionController(UserSawQuestionRepository _repository, IMapper _mapper)
    {
        repository = _repository;
        mapper = _mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    [OpenApiOperation("User saw question" + nameof(GetAll))]
    public ActionResult<List<UserSawQuestionModel>> GetAll()
    {
        List<UserSawQuestionEntity> models = (List<UserSawQuestionEntity>)repository.GetAll();
        var listModels = mapper.Map<List<UserSawQuestionModel>>(models);
        return Ok(listModels);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("question/{questionId}")]
    [OpenApiOperation("User saw question" + nameof(GetAllFromQuestion))]
    public ActionResult<List<UserSawQuestionModel>> GetAllFromQuestion(Guid questionId)
    {
        List<UserSawQuestionEntity> entities = (List<UserSawQuestionEntity>)repository.GetAll();
        List<UserSawQuestionEntity> entitiesForQuestion = entities.Where(userSawQuestion => userSawQuestion.QuestionId == questionId).ToList();
        var models = mapper.Map<List<UserSawQuestionModel>>(entitiesForQuestion);
        return Ok(models);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [OpenApiOperation("User saw question" + nameof(GetById))]
    public ActionResult<UserSawQuestionModel> GetById(Guid id)
    {
        var entity = repository.GetByID(id);
        var detailModel = mapper.Map<UserSawQuestionModel>(entity);
        return Ok(detailModel);
    }

    [HttpDelete("{id}")]
    [OpenApiOperation("User saw question" + nameof(Delete))]
    public ActionResult Delete(Guid id)
    {
        repository.Delete(id);
        return Ok();
    }

    [HttpPut]
    [OpenApiOperation("User saw question" + nameof(Update))]
    public ActionResult Update(UserSawQuestionModel model)
    {
        var entity = mapper.Map<UserSawQuestionEntity>(model);
        repository.Update(entity);
        return Ok();
    }

    [HttpPost]
    [OpenApiOperation("User saw question" + nameof(Insert))]
    public ActionResult<UserSawQuestionModel> Insert(UserSawQuestionModel model)
    {
        var entity = mapper.Map<UserSawQuestionEntity>(model);
        entity = repository.Insert(entity);
        if(entity == null)
        {
            return BadRequest();
        }
        var detailModel = mapper.Map<UserSawQuestionModel>(entity);
        return Ok(detailModel);
    }

}
