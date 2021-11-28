using Fituska.BL.Repositories;
using Fituska.DAL.Entities.Interfaces;
using Fituska.Shared.Models.Question;
using NSwag.Annotations;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
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

    [HttpGet]
    [OpenApiOperation("User saw answer" + nameof(GetAll))]
    public ActionResult<List<UserSawQuestionModel>> GetAll()
    {
        List<UserSawQuestionEntity> models = (List<UserSawQuestionEntity>)repository.GetAll();
        var listModels = mapper.Map<List<UserSawQuestionModel>>(models);
        return Ok(listModels);
    }

    [HttpGet("{id}")]
    [OpenApiOperation("User saw answer" + nameof(GetById))]
    public ActionResult<UserSawQuestionModel> GetById(Guid id)
    {
        var entity = repository.GetByID(id);
        var detailModel = mapper.Map<UserSawQuestionModel>(entity);
        return Ok(detailModel);
    }

    [HttpDelete]
    [OpenApiOperation("User saw answer" + nameof(Delete))]
    public ActionResult Delete(Guid id)
    {
        repository.Delete(id);
        return Ok();
    }

    [HttpPut]
    [OpenApiOperation("User saw answer" + nameof(Update))]
    public ActionResult Update(IEntity entity)
    {
        repository.Update((UserSawQuestionEntity)entity);
        return Ok();
    }

    [HttpPost]
    [OpenApiOperation("User saw answer" + nameof(Insert))]
    public ActionResult<UserSawQuestionModel> Insert(UserSawQuestionModel model)
    {
        var entity = mapper.Map<UserSawQuestionEntity>(model);
        entity = repository.Insert(entity);
        if(entity == null)
        {
            return BadRequest(entity);
        }
        var detailModel = mapper.Map<UserSawQuestionModel>(entity);
        return Ok(detailModel);
    }
}
