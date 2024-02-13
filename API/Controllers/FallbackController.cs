using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// It is responsible for returning the index.html file located in the wwwroot directory.
    /// </summary>
    public class FallbackController : Controller
    {
        /// <summary>
        ///  Handles requests to the default route and serves the index.html file.
        /// </summary>
        /// <returns>Status code with contents of the index.html file.</returns>
        public ActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }
}