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
        List<CourseEntity> entities = repository.GetAll().ToList();
        var listDetailModels = mapper.Map<List<CourseListModel>>(entities);
        return Ok(listDetailModels);
    }

    [AllowAnonymous]
    [HttpGet("{courseUrl}")]
    [OpenApiOperation("Course" + nameof(GetByUrl))]
    public ActionResult<CourseDetailModel> GetByUrl(string courseUrl)
    {
        var entity = repository.GetByUrl(courseUrl);
        var model = mapper.Map<CourseDetailModel>(entity);
        if(model == null)
        {
            return BadRequest();
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
        if(entity == null)
        {
            return BadRequest();
        }
        var result = mapper.Map<CourseListModel>(entity);
        return Ok(result);
    }

    [Route("foredit/{url}")]
    [HttpGet]
    public ActionResult<CourseNewModel> GetForEdit(string url)
    {
        var entity = repository.GetByUrl(url);
        if (entity == null)
        {
            return BadRequest();
        }
        return Ok(mapper.Map<CourseNewModel>(entity));
    }

    [AllowAnonymous]
    [HttpGet("user/{lecturerId}")]
    public ActionResult<List<CourseListModel>> GetAllForLecturer(Guid lecturerId)
    {
        List<CourseEntity> entities = repository.GetAll().Where(course => course.LecturerId == lecturerId).ToList();
        var listDetailModels = mapper.Map<List<CourseListModel>>(entities);
        return Ok(listDetailModels);
    }

    [HttpGet("forapproval")]
    public ActionResult<List<CourseListModel>> GetAllForModeratorApprove()
    {
        List<CourseEntity> entities = repository.GetAll().Where(course => !course.ModeratorApproved).ToList();
        var listDetailModels = mapper.Map<List<CourseListModel>>(entities);
        return Ok(listDetailModels);
    }

    [HttpGet("approve/{id}")]
    public ActionResult ModeratorApprovesCourse(Guid id)
    {
        var entity = repository.GetByID(id);
        entity.ModeratorApproved = true;
        repository.Update(entity);
        return Ok();
    }
}
