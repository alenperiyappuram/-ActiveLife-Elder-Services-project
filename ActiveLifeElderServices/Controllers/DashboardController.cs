using Microsoft.AspNetCore.Authorization; // For [Authorize] attribute
using Microsoft.AspNetCore.Mvc;

namespace ActiveLifeElderServices.Controllers
{
    // This attribute ensures only authenticated users can access actions in this controller
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: /Dashboard/Index
        public IActionResult Index()
        {
            ViewData["Title"] = "Dashboard"; // Set page title
            return View();
        }
    }
}
