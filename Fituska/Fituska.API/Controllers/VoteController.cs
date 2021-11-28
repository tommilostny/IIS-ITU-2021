using Fituska.BL.Repositories;
using Fituska.DAL.Entities.Interfaces;
using Fituska.Shared.Models.Vote;
using NSwag.Annotations;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VoteController : ControllerBase
{
    private readonly VoteRepository repository;
    private readonly IMapper mapper;
    public VoteController(VoteRepository _repository, IMapper _mapper)
    {
        repository = _repository;
        mapper = _mapper;
    }

    [HttpGet]
    [OpenApiOperation("Vote" + nameof(GetAll))]
    public ActionResult<List<VoteModel>> GetAll()
    {
        List<VoteEntity> entities = (List<VoteEntity>)repository.GetAll();
        var models = mapper.Map<List<VoteModel>>(entities);
        return Ok(models);
    }

    [HttpGet("{id}")]
    [OpenApiOperation("Vote" + nameof(GetById))]
    public ActionResult<VoteModel> GetById(Guid id)
    {
        var entity = repository.GetByID(id);
        var detailModel = mapper.Map<VoteModel>(entity);
        return Ok(detailModel);
    }

    [HttpDelete]
    [OpenApiOperation("Vote" + nameof(Delete))]
    public ActionResult Delete(Guid id)
    {
        repository.Delete(id);
        return Ok();
    }

    [HttpPut]
    [OpenApiOperation("Vote" + nameof(Update))]
    public ActionResult Update(VoteModel model)
    {
        var entity = mapper.Map<VoteEntity>(model);
        entity = repository.Update(entity);
        if(entity == null)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPost]
    [OpenApiOperation("Vote" + nameof(Insert))]
    public ActionResult<VoteModel> Insert(VoteModel model)
    {
        var entity = mapper.Map<VoteEntity>(model);
        entity = repository.Insert(entity);
        if(entity == null)
        {
            return BadRequest(entity);
        }
        var detailModel = mapper.Map<VoteModel>(entity);
        return Ok(detailModel);
    }
}
