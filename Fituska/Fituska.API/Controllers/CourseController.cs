using Fituska.BL.Repositories;
using Fituska.Shared.Models.Course;
using NSwag.Annotations;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly CourseRepository repository;
    private readonly IMapper mapper;
    public CourseController(CourseRepository _repository, IMapper _mapper)
    {
        repository = _repository;
        mapper = _mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    [OpenApiOperation("Course" + nameof(GetAll))]
    public ActionResult<List<CourseListModel>> GetAll()
    {
        List<CourseAttendanceEntity> entities = (List<CourseAttendanceEntity>)repository.GetAll();
        var listDetailModels = mapper.Map<List<CourseListModel>>(entities);
        return Ok(listDetailModels);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [OpenApiOperation("Course" + nameof(GetById))]
    public ActionResult<CourseListModel> GetById(Guid id)
    {
        var entity = repository.GetByID(id);
        var model = mapper.Map<CourseListModel>(entity);
        if(model == null)
        {
            return BadRequest(model);
        }
        return Ok(model);
    }

    [HttpDelete("{id}")]
    [OpenApiOperation("Course" + nameof(Delete))]
    public ActionResult Delete(Guid id)
    {
        repository.Delete(id);
        return Ok();
    }

    [HttpPut]
    [OpenApiOperation("Course" + nameof(Update))]
    public ActionResult Update(CourseNewModel model)
    {
        CourseEntity entity = mapper.Map<CourseEntity>(model);
        entity = repository.Update(entity);
        if(entity == null)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPost]
    [OpenApiOperation("Course" + nameof(Insert))]
    public ActionResult<CourseListModel> Insert(CourseNewModel model)
    {
        var entity = mapper.Map<CourseEntity>(model);
        entity = repository.Insert(entity);
        if(entity == null){
            return BadRequest(entity);
        }
        var detailModel = mapper.Map<CourseListModel>(model);
        return Ok(detailModel);
    }
}
