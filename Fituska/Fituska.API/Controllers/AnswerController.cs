using Fituska.BL.Repositories;
using Fituska.DAL.Entities.Interfaces;
using Fituska.Shared.Models.Answer;
using NSwag.Annotations;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnswerController : ControllerBase
{
    private readonly AnswerRepository answerRepository;
    private readonly IMapper mapper;
    public AnswerController(AnswerRepository _answerRepository , IMapper _mapper)
    {
        answerRepository = _answerRepository;
        mapper = _mapper;
    }

    [HttpGet]
    [OpenApiOperation("Answer" + nameof(GetAll))]
    public ActionResult<List<AnswerListModel>> GetAll()
    {
        List<AnswerEntity> categories = (List<AnswerEntity>)answerRepository.GetAll();
        var categoriesListModels = mapper.Map<List<AnswerListModel>>(categories);
        return Ok(categoriesListModels);
    }

    [HttpGet("{id}")]
    [OpenApiOperation("Answer" + nameof(GetById))]
    public ActionResult<AnswerDetailModel> GetById(Guid id)
    {
        var entity = answerRepository.GetByID(id);
        var detailModel = mapper.Map<AnswerDetailModel>(entity);
        return Ok(detailModel);
    }

    [HttpDelete]
    [OpenApiOperation("Answer" + nameof(Delete))]
    public ActionResult Delete(Guid id)
    {
        answerRepository.Delete(id);
        return Ok();
    }

    [HttpPut]
    [OpenApiOperation("Answer" + nameof(Update))]
    public ActionResult Update(IEntity entity)
    {
        answerRepository.Update(entity);
        return Ok();
    }

    [HttpPost]
    [OpenApiOperation("Answer" + nameof(Insert))]
    public ActionResult<AnswerDetailModel> Insert(IEntity entity)
    {
        var IEntity = answerRepository.Insert(entity);
        var detailModel = mapper.Map<AnswerDetailModel>(IEntity);
        return Ok(detailModel);
    }
}
