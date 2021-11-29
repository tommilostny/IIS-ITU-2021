using Fituska.BL.Repositories;
using Fituska.Shared.Models.Answer;
using NSwag.Annotations;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class UserSawAnswerController : ControllerBase
{
    private readonly UserSawAnswerRespository repository;
    private readonly IMapper mapper;
    public UserSawAnswerController(UserSawAnswerRespository _repository, IMapper _mapper)
    {
        repository = _repository;
        mapper = _mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    [OpenApiOperation("User saw answer" + nameof(GetAll))]
    public ActionResult<List<UserSawAnswerModel>> GetAll()
    {
        List<UserSawAnswerEntity> models = (List<UserSawAnswerEntity>)repository.GetAll();
        var listModels = mapper.Map<List<UserSawAnswerModel>>(models);
        return Ok(listModels);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [OpenApiOperation("User saw answer" + nameof(GetById))]
    public ActionResult<UserSawAnswerModel> GetById(Guid id)
    {
        var entity = repository.GetByID(id);
        var detailModel = mapper.Map<UserSawAnswerModel>(entity);
        return Ok(detailModel);
    }

    [HttpDelete("{id}")]
    [OpenApiOperation("User saw answer" + nameof(Delete))]
    public ActionResult Delete(Guid id)
    {
        repository.Delete(id);
        return Ok();
    }

    [HttpPut]
    [OpenApiOperation("User saw answer" + nameof(Update))]
    public ActionResult Update(UserSawAnswerModel model)
    {
        var entity = mapper.Map<UserSawAnswerEntity>(model);
        repository.Update(entity);
        return Ok();
    }

    [HttpPost]
    [OpenApiOperation("User saw answer" + nameof(Insert))]
    public ActionResult<UserSawAnswerModel> Insert(UserSawAnswerModel model)
    {
        var entity = mapper.Map<UserSawAnswerEntity>(model);
        entity = repository.Insert(entity);
        if(entity == null)
        {
            return BadRequest(entity);
        }
        var detailModel = mapper.Map<UserSawAnswerModel>(entity);
        return Ok(detailModel);
    }
}
