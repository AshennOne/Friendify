using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
/// <summary>
/// The UploadController class handles file uploads to storage cloud.
/// </summary>
public class UploadController : BaseApiController
{
    /// <summary>
    /// An instance of IConfiguration used for accessing configuration settings.
    /// </summary>
    private readonly IConfiguration _configuration;
    /// <summary>
    /// Initializes a new instance of the UploadController class.
    /// </summary>
    /// <param name="configuration">An instance of IConfiguration used for accessing configuration settings.</param>
    public UploadController(IConfiguration configuration)
    {
        _configuration = configuration;

    }
    /// <summary>
    /// Handles post requests for file uploads.
    /// </summary>
    /// <returns>Status code with URL of the uploaded image.</returns>
    [HttpPost]
    public ActionResult<string> Post()
    {
        Account account = new Account(_configuration["CloudName"], _configuration["ApiKey"], _configuration["ApiSecret"]);
        Cloudinary _cloudinary = new Cloudinary(account);
        var file = Request.Form.Files[0];
        var uploadResult = new ImageUploadResult();
        if (file.Length < 0) return NotFound("File not found");
        using (var stream = file.OpenReadStream())
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.Name, stream),
                Folder = "cinemaster",
                UploadPreset = "vkecuura",
            };
            uploadResult = _cloudinary.Upload(uploadParams);
        }
        var response = new { imageUrl = uploadResult.SecureUrl.ToString() };
        return Ok(response);
    }
}
