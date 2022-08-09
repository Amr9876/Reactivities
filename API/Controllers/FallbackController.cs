using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[AllowAnonymous]
public class FallbackController : Controller
{
    public IActionResult Index() 
    {
        string physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "dist", "index.html");
        return PhysicalFile(physicalPath, MediaTypeNames.Text.Html);
    }
}
