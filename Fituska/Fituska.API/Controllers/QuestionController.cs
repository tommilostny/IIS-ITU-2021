using Fituska.BL.Repositories;
using Fituska.Shared.Models.Question;
using NSwag.Annotations;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class QuestionController : ControllerBase
{
    private readonly QuestionRepository repository;
    private readonly IMapper mapper;
    public QuestionController(QuestionRepository _repository, IMapper _mapper)
    {
        repository = _repository;
        mapper = _mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    [OpenApiOperation("Question" + nameof(GetAll))]
    public ActionResult<List<QuestionListModel>> GetAll()
    {
        List<QuestionEntity> questions = (List<QuestionEntity>)repository.GetAll();
        var listModels = mapper.Map<List<QuestionListModel>>(questions);
        return Ok(listModels);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [OpenApiOperation("Question" + nameof(GetById))]
    public ActionResult<QuestionDetailModel> GetById(Guid id)
    {
        var entity = repository.GetByID(id);
        var detailModel = mapper.Map<QuestionDetailModel>(entity);
        return Ok(detailModel);
    }

    [HttpDelete("{id}")]
    [OpenApiOperation("Question" + nameof(Delete))]
    public ActionResult Delete(Guid id)
    {
        repository.Delete(id);
        return Ok();
    }

    [HttpPut]
    [OpenApiOperation("Question" + nameof(Update))]
    public ActionResult Update(QuestionNewModel model)
    {
        var entity = mapper.Map<QuestionEntity>(model);
        repository.Update(entity);
        return Ok();
    }

    [HttpPost]
    [OpenApiOperation("Question" + nameof(Insert))]
    public ActionResult<QuestionDetailModel> Insert(QuestionNewModel questionModel)
    {
        var entity = mapper.Map<QuestionEntity>(questionModel);
        entity = repository.Insert(entity);
        if(entity == null)
        {
            return BadRequest(entity);
        }
        var detailModel = mapper.Map<QuestionDetailModel>(entity);
        return Ok(detailModel);
    }
}
