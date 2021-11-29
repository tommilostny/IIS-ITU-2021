using Fituska.BL.Repositories;
using Fituska.Shared.Models.Answer;
using NSwag.Annotations;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class AnswerController : ControllerBase
{
    private readonly AnswerRepository repository;
    private readonly IMapper mapper;
    public AnswerController(AnswerRepository _repository , IMapper _mapper)
    {
        repository = _repository;
        mapper = _mapper;
    }

    [HttpDelete("{id}")]
    [OpenApiOperation("Answer" + nameof(Delete))]
    public ActionResult Delete(Guid id)
    {
        repository.Delete(id);
        return Ok();
    }

    [HttpPut]
    [OpenApiOperation("Answer" + nameof(Update))]
    public ActionResult Update(AnswerNewModel model)
    {
        AnswerEntity entity = mapper.Map<AnswerEntity>(model);
        entity = repository.Update(entity);
        if(entity == null)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPost]
    [OpenApiOperation("Answer" + nameof(Insert))]
    public ActionResult<AnswerDetailModel> Insert(AnswerNewModel model)
    {
        var entity = mapper.Map<AnswerEntity>(model);
        entity = repository.Insert(entity);
        if(entity == null)
        {
            return BadRequest();
        }
        var detailModel = mapper.Map<AnswerDetailModel>(entity);
        return Ok(detailModel);
    }
}
