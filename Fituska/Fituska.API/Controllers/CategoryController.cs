using Fituska.BL.Repositories;
using Fituska.DAL.Entities.Interfaces;
using Fituska.Shared.Models.Category;
using NSwag.Annotations;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly CategoryRepository categoryRepository;
    private readonly IMapper mapper;
    public CategoryController(CategoryRepository _categoryRepository , IMapper _mapper)
    {
        categoryRepository = _categoryRepository;
        mapper = _mapper;
    }

    [HttpGet]
    public ActionResult<List<CategoryListModel>> GetAll()
    {
        List<CategoryEntity> categories = (List<CategoryEntity>)categoryRepository.GetAll();
        var categoriesListModels = mapper.Map<List<CategoryListModel>>(categories);
        return Ok(categoriesListModels);
    }

    [HttpGet("{id}")]
    [OpenApiOperation("Category" + nameof(GetById))]
    public ActionResult<CategoryListModel> GetById(Guid id)
    {
        var category = categoryRepository.GetByID(id);
        var categoryDetailModel = mapper.Map<CategoryDetailModel>(category);
        return Ok(categoryDetailModel);
    }

    [HttpDelete]
    [OpenApiOperation("Category" + nameof(Delete))]
    public ActionResult<CategoryListModel> Delete(Guid id)
    {
        categoryRepository.Delete(id);
        return Ok();
    }

    [HttpPut]
    [OpenApiOperation("Category" + nameof(Update))]
    public ActionResult<CategoryListModel> Update(IEntity entity)
    {
        categoryRepository.Update(entity);
        return Ok();
    }

    [HttpPost] 
    [OpenApiOperation("Category" + nameof(Insert))]
    public ActionResult<CategoryDetailModel> Insert(IEntity entity)
    {
        var Entity = categoryRepository.Insert(entity);
        var detailModel = mapper.Map<CategoryDetailModel>(Entity);
        return Ok();
    }
}
