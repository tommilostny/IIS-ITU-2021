using Fituska.BL.Repositories;
using Fituska.DAL.Entities.Interfaces;
using Fituska.Shared.Models.CourseAttendance;
using NSwag.Annotations;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseAttendanceController : ControllerBase
{
    private readonly CourseAttendanceRepository repository;
    private readonly IMapper mapper;
    public CourseAttendanceController(CourseAttendanceRepository _repository, IMapper _mapper)
    {
        repository = _repository;
        mapper = _mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    [OpenApiOperation("Course attendence" + nameof(GetAll))]
    public ActionResult<List<CourseAttendanceListModel>> GetAll()
    {
        List<CourseAttendanceEntity> entities = (List<CourseAttendanceEntity>)repository.GetAll();
        var listDetailModels = mapper.Map<List<CourseAttendanceListModel>>(entities);
        return Ok(listDetailModels);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [OpenApiOperation("Course attendence" + nameof(GetById))]
    public ActionResult<CourseAttendanceListModel> GetById(Guid id)
    {
        var entity = repository.GetByID(id);
        var model = mapper.Map<CourseAttendanceListModel>(entity);
        if(model == null)
        {
            return BadRequest(model);
        }
        return Ok(model);
    }

    [HttpDelete]
    [OpenApiOperation("Course attendence" + nameof(Delete))]
    public ActionResult Delete(Guid id)
    {
        repository.Delete(id);
        return Ok();
    }

    [HttpPut]
    [OpenApiOperation("Course attendence" + nameof(Update))]
    public ActionResult Update(CourseAttendanceNewModel model)
    {
        CourseAttendanceEntity entity = mapper.Map<CourseAttendanceEntity>(model);
        entity = repository.Update(entity);
        if(entity == null)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPost]
    [OpenApiOperation("Course attendence" + nameof(Insert))]
    public ActionResult<CourseAttendanceListModel> Insert(CourseAttendanceNewModel model)
    {
        var entity = mapper.Map<CourseAttendanceEntity>(model);
        entity = repository.Insert(entity);
        if(entity == null){
            return BadRequest(entity);
        }
        var detailModel = mapper.Map<CourseAttendanceListModel>(model);
        return Ok(detailModel);
    }
}
