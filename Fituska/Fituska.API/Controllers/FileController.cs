using Fituska.BL.Repositories;
using Fituska.Shared.Models.File;
using Microsoft.AspNetCore.Identity;
using NSwag.Annotations;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class FileController : ControllerBase
{
    private readonly FileRepository repository;
    private readonly UserManager<UserEntity> userManager;
    private readonly IMapper mapper;
    public FileController(FileRepository _repository, UserManager<UserEntity> _userManager, IMapper _mapper)
    {
        repository = _repository;
        userManager = _userManager;
        mapper = _mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    [OpenApiOperation("File" + nameof(GetAll))]
    public ActionResult<List<FileListModel>> GetAll()
    {
        List<FileEntity> entities = (List<FileEntity>)repository.GetAll();
        var listModels = mapper.Map<List<FileListModel>>(entities);
        return Ok(listModels);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [OpenApiOperation("File" + nameof(GetById))]
    public ActionResult<FileModelBase> GetById(Guid id)
    {
        var entity = repository.GetByID(id);
        var detailModel = mapper.Map<FileModelBase>(entity);
        if(detailModel != null)
        {
            return BadRequest();
        }
        return Ok(detailModel);
    }

    [HttpDelete("{id}")]
    [OpenApiOperation("File" + nameof(Delete))]
    public ActionResult Delete(Guid id)
    {
        repository.Delete(id);
        return Ok();
    }

    [Route("answer")]
    [HttpPut]
    [OpenApiOperation("File" + nameof(UpdateAnswerFile))]
    public ActionResult UpdateAnswerFile(FileAnswerModel model)
    {
        var entity = mapper.Map<FileEntity>(model);
        entity = repository.Update(entity);
        if (entity == null)
        {
            return BadRequest();
        }
        return Ok();
    }

    [Route("answer")]
    [HttpPost]
    [OpenApiOperation("File" + nameof(InsertAnswerFile))]
    public ActionResult<FileAnswerModel> InsertAnswerFile(FileAnswerModel model)
    {
        var entity = mapper.Map<FileEntity>(model);
        entity = repository.Insert(entity);
        if(entity == null)
        {
            return BadRequest();
        }
        var detailModel = mapper.Map<FileAnswerModel>(entity);
        return Ok(detailModel);
    }

    [Route("comment")]
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

    [Route("comment")]
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

    [Route("question")]
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

    [Route("question")]
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

    [Route("user")]
    [HttpPost]
    [OpenApiOperation("File" + nameof(InsertUserFile))]
    public async Task<ActionResult> InsertUserFile(FileUserModel model)
    {
        var userEntity = await userManager.FindByIdAsync(model.UserId.ToString());
        if (userEntity is null)
        {
            return BadRequest();
        }
        userEntity.Photo = model.Content;
        userEntity.PhotoFileName = model.Name;

        await userManager.UpdateAsync(userEntity);
        return Ok();
    }

    [AllowAnonymous]
    [Route("user/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetUserPhoto(string id)
    {
        var userEntity = await userManager.FindByIdAsync(id.ToString());
        if (userEntity is null)
        {
            return BadRequest();
        }
        return Ok(mapper.Map<FileUserModel>(userEntity));
    }
}
