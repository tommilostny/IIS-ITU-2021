﻿namespace Fituska.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageUploadController : ControllerBase
{
    private readonly IWebHostEnvironment environment;

    public ImageUploadController(IWebHostEnvironment environment)
    {
        this.environment = environment;
    }

    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] IFormFile file)
    {
        if (file.Length > 0)
        {
            var trustedFileName = Path.GetRandomFileName();
            var path = Path.Combine(environment.ContentRootPath, environment.EnvironmentName, "images", trustedFileName);

            await using FileStream fs = new(path, FileMode.Create);
            await file.CopyToAsync(fs);

            return Ok(file.FileName);
        }
        return BadRequest();
    }
}
