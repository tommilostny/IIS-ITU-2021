using Fituska.BL.Repositories;
using Fituska.DAL.Entities.Interfaces;
using Fituska.Shared.Models.Question;
using NSwag.Annotations;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
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

    [HttpGet]
    [OpenApiOperation("Question" + nameof(GetAll))]
    public ActionResult<List<QuestionListModel>> GetAll()
    {
        List<QuestionEntity> questions = (List<QuestionEntity>)repository.GetAll();
        var listModels = mapper.Map<List<QuestionListModel>>(questions);
        return Ok(listModels);
    }

    [HttpGet("{id}")]
    [OpenApiOperation("Question" + nameof(GetById))]
    public ActionResult<QuestionDetailModel> GetById(Guid id)
    {
        var entity = repository.GetByID(id);
        var detailModel = mapper.Map<QuestionDetailModel>(entity);
        return Ok(detailModel);
    }

    [HttpDelete]
    [OpenApiOperation("Question" + nameof(Delete))]
    public ActionResult Delete(Guid id)
    {
        repository.Delete(id);
        return Ok();
    }

    [HttpPut]
    [OpenApiOperation("Question" + nameof(Update))]
    public ActionResult Update(IEntity entity)
    {
        repository.Update(entity);
        return Ok();
    }

    [HttpPost]
    [OpenApiOperation("Question" + nameof(Insert))]
    public ActionResult<QuestionDetailModel> Insert(IEntity entity)
    {
        var IEntity = repository.Insert(entity);
        var detailModel = mapper.Map<QuestionDetailModel>(IEntity);
        return Ok(detailModel);
    }
}
