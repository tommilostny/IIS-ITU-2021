using Fituska.BL.Repositories;
using Fituska.DAL.Entities.Interfaces;
using Fituska.Shared.Models.Category;
using NSwag.Annotations;

namespace Fituska.API.Controllers;

[Authorize()]
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly CategoryRepository repository;
    private readonly IMapper mapper;
    public CategoryController(CategoryRepository _repository , IMapper _mapper)
    {
        repository = _repository;
        mapper = _mapper;
    }

    [HttpGet]
    public ActionResult<List<CategoryListModel>> GetAll()
    {
        List<CategoryEntity> categories = (List<CategoryEntity>)repository.GetAll();
        var categoriesListModels = mapper.Map<List<CategoryListModel>>(categories);
        return Ok(categoriesListModels);
    }

    [HttpGet("{id}")]
    [OpenApiOperation("Category" + nameof(GetById))]
    public ActionResult<CategoryListModel> GetById(Guid id)
    {
        var category = repository.GetByID(id);
        var categoryDetailModel = mapper.Map<CategoryDetailModel>(category);
        return Ok(categoryDetailModel);
    }

    [HttpDelete]
    [OpenApiOperation("Category" + nameof(Delete))]
    public ActionResult<CategoryListModel> Delete(Guid id)
    {
        repository.Delete(id);
        return Ok();
    }

    [HttpPut]
    [OpenApiOperation("Category" + nameof(Update))]
    public ActionResult<CategoryDetailModel> Update(CategoryNewModel categoryModel)
    {
        var entity = mapper.Map<CategoryEntity>(categoryModel);
        var detailModel = mapper.Map<CategoryDetailModel>(categoryModel);
        entity = repository.Update(entity);
        if(entity == null)
        {
            return BadRequest(detailModel);
        }
        return Ok(detailModel);
    }

    [HttpPost] 
    [OpenApiOperation("Category" + nameof(Insert))]
    public ActionResult<CategoryDetailModel> Insert(CategoryNewModel categoryModel)
    {
        var entity = mapper.Map<CategoryEntity>(categoryModel);
        var detailModel = mapper.Map<CategoryDetailModel>(categoryModel);
        entity = repository.Insert(entity);
        if(entity == null)
        {
            return BadRequest(detailModel);
        }
        return Ok(detailModel);
    }
}
