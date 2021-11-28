﻿using Fituska.BL.Repositories;
using Fituska.DAL.Entities.Interfaces;
using Fituska.Shared.Models.Comment;
using NSwag.Annotations;

namespace Fituska.API.Controllers;

[Route("api/[controller]")]
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
            return BadRequest(detailModel);
        }
        return Ok(detailModel);
    }

    [HttpDelete]
    [OpenApiOperation("Comment" + nameof(Delete))]
    public ActionResult Delete(Guid id)
    {
        repository.Delete(id);
        return Ok();
    }

    [HttpPut]
    [OpenApiOperation("Comment" + nameof(Update))]
    public ActionResult Update(CommentNewModel model)
    {
        CommentEntity entity = mapper.Map<CommentEntity>(model);
        entity = repository.Update(entity);
        if(entity == null)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPost]
    [OpenApiOperation("Comment" + nameof(Insert))]
    public ActionResult<CommentDetailModel> Insert(CommentNewModel model)
    {
        var entity = mapper.Map<CommentEntity>(model);
        entity = repository.Insert(entity);
        if(entity == null){
            return BadRequest(entity);
        }
        var detailModel = mapper.Map<CommentDetailModel>(model);
        return Ok(detailModel);
    }
}