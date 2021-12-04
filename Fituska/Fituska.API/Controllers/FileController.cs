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
        var fileModel = mapper.Map<FileModelBase>(entity);
        if(fileModel is null)
        {
            return BadRequest();
        }
        return Ok(fileModel);
    }

    [HttpDelete("{id}")]
    [OpenApiOperation("File" + nameof(Delete))]
    public ActionResult Delete(Guid id)
    {
        repository.Delete(id);
        return Ok();
    }

    [Route("answer")]
    [HttpPost]
    [OpenApiOperation("File" + nameof(InsertAnswerFile))]
    public ActionResult<FileListModel> InsertAnswerFile(FileAnswerModel model)
    {
        var entity = mapper.Map<FileEntity>(model);
        entity = repository.Insert(entity);
        if(entity == null)
        {
            return BadRequest();
        }
        var listModel = mapper.Map<FileListModel>(entity);
        return Ok(listModel);
    }

    [Route("question")]
    [HttpPost]
    [OpenApiOperation("File" + nameof(InsertQuestionFile))]
    public ActionResult<FileListModel> InsertQuestionFile(FileQuestionModel model)
    {
        var entity = mapper.Map<FileEntity>(model);
        entity = repository.Insert(entity);
        if (entity == null)
        {
            return BadRequest();
        }
        var listModel = mapper.Map<FileListModel>(entity);
        return Ok(listModel);
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
    public async Task<IActionResult> GetUserPhoto(Guid id)
    {
        var userEntity = await userManager.FindByIdAsync(id.ToString());
        if (userEntity is null)
        {
            return BadRequest();
        }
        return Ok(mapper.Map<FileUserModel>(userEntity));
    }
}
