using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
/// <summary>
/// Base API controller providing common configurations for other controllers.
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{

}
