using Fituska.BL.Repositories;
using Fituska.DAL.Entities.Interfaces;
using Fituska.Shared.Models.File;
using NSwag.Annotations;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly FileRepository repository;
    private readonly IMapper mapper;
    public FileController(FileRepository _repository, IMapper _mapper)
    {
        repository = _repository;
        mapper = _mapper;
    }

    [HttpGet]
    [OpenApiOperation("File" + nameof(GetAll))]
    public ActionResult<List<FileListModel>> GetAll()
    {
        List<FileEntity> entities = (List<FileEntity>)repository.GetAll();
        var listModels = mapper.Map<List<FileListModel>>(entities);
        return Ok(listModels);
    }

    [HttpGet("{id}")]
    [OpenApiOperation("File" + nameof(GetById))]
    public ActionResult<FileModelBase> GetById(Guid id)
    {
        var entity = repository.GetByID(id);
        var detailModel = mapper.Map<FileModelBase>(entity);
        if(detailModel != null)
        {
            return BadRequest(detailModel);
        }
        return Ok(detailModel);
    }

    [HttpDelete]
    [OpenApiOperation("File" + nameof(Delete))]
    public ActionResult Delete(Guid id)
    {
        repository.Delete(id);
        return Ok();
    }

    [HttpPut]
    [OpenApiOperation("File" + nameof(UpdateAnswerFile))]
    public ActionResult UpdateAnswerFile(FileAnswerModel model)
    {
        var entity = mapper.Map<FileEntity>(model);
        entity = repository.Update(entity);
        if (entity == null)
        {
            return BadRequest(entity);
        }
        return Ok();
    }

    [HttpPost]
    [OpenApiOperation("File" + nameof(InsertAnswerFile))]
    public ActionResult<FileAnswerModel> InsertAnswerFile(FileAnswerModel model)
    {
        var entity = mapper.Map<FileEntity>(model);
        entity = repository.Insert(entity);
        if(entity == null)
        {
            return BadRequest(entity);
        }
        var detailModel = mapper.Map<FileAnswerModel>(entity);
        return Ok(detailModel);
    }

    [HttpPut]
    [OpenApiOperation("File" + nameof(UpdateCommentFile))]
    public ActionResult UpdateCommentFile(FileCommentModel model)
    {
        var entity = mapper.Map<FileEntity>(model);
        entity = repository.Update(entity);
        if (entity == null)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPost]
    [OpenApiOperation("File" + nameof(InsertCommentFile))]
    public ActionResult<FileCommentModel> InsertCommentFile(FileCommentModel model)
    {
        var entity = mapper.Map<FileEntity>(model);
        entity = repository.Insert(entity);
        if (entity == null)
        {
            return BadRequest();
        }
        var detailModel = mapper.Map<FileCommentModel>(entity);
        return Ok(detailModel);
    }

    [HttpPut]
    [OpenApiOperation("File" + nameof(UpdateQuestionFile))]
    public ActionResult UpdateQuestionFile(FileQuestionModel model)
    {
        var entity = mapper.Map<FileEntity>(model);
        entity = repository.Update(entity);
        if (entity == null)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPost]
    [OpenApiOperation("File" + nameof(InsertQuestionFile))]
    public ActionResult<FileQuestionModel> InsertQuestionFile(FileQuestionModel model)
    {
        var entity = mapper.Map<FileEntity>(model);
        entity = repository.Insert(entity);
        if (entity == null)
        {
            return BadRequest();
        }
        var detailModel = mapper.Map<FileQuestionModel>(entity);
        return Ok(detailModel);
    }

    [HttpPut]
    [OpenApiOperation("File" + nameof(UpdateUserFile))]
    public ActionResult UpdateUserFile(FileUserModel model)
    {
        var entity = mapper.Map<FileEntity>(model);
        entity = repository.Update(entity);
        if (entity == null)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPost]
    [OpenApiOperation("File" + nameof(InsertUserFile))]
    public ActionResult<FileUserModel> InsertUserFile(FileUserModel model)
    {
        var entity = mapper.Map<FileEntity>(model);
        entity = repository.Insert(entity);
        if (entity == null)
        {
            return BadRequest();
        }
        var detailModel = mapper.Map<FileUserModel>(entity);
        return Ok(detailModel);
    }
}
