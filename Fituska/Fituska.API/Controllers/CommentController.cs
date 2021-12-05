using Fituska.BL.Repositories;
using Fituska.Shared.Models.Comment;
using NSwag.Annotations;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly CommentRepository repository;
    private readonly IMapper mapper;
    public CommentController(CommentRepository _repository, IMapper _mapper)
    {
        repository = _repository;
        mapper = _mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    [OpenApiOperation("Comment" + nameof(GetAll))]
    public ActionResult<List<CommentDetailModel>> GetAll()
    {
        List<CommentEntity> entities = (List<CommentEntity>)repository.GetAll();
        var models = mapper.Map<List<CommentDetailModel>>(entities);
        return Ok(models);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [OpenApiOperation("Comment" + nameof(GetById))]
    public ActionResult<CommentDetailModel> GetById(Guid id)
    {
        var entity = repository.GetByID(id);
        var detailModel = mapper.Map<CommentDetailModel>(entity);
        if(detailModel == null)
        {
            return BadRequest();
        }
        return Ok(detailModel);
    }

    [HttpDelete("{id}")]
    [OpenApiOperation("Comment" + nameof(Delete))]
    public ActionResult Delete(Guid id)
    {
        repository.Delete(id);
        return Ok();
    }

    [HttpPut]
    [OpenApiOperation("Comment" + nameof(Update))]
    public ActionResult<CommentDetailModel> Update(CommentNewModel model)
    {
        var entity = repository.GetByID(model.Id);

        entity.Text = model.Text;
        entity.ModifiedTime = DateTime.UtcNow;

        entity = repository.Update(entity);

        if(entity == null)
        {
            return BadRequest();
        }
        return Ok(mapper.Map<CommentDetailModel>(entity));
    }

    [HttpPost]
    [OpenApiOperation("Comment" + nameof(Insert))]
    public ActionResult<CommentDetailModel> Insert(CommentNewModel model)
    {
        var entity = mapper.Map<CommentEntity>(model);
        entity = repository.Insert(entity);
        if(entity == null)
        {
            return BadRequest();
        }
        var detailModel = mapper.Map<CommentDetailModel>(entity);
        return Ok(detailModel);
    }
}
