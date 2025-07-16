using ActiveLifeElderServices.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq; // Add this using directive for .ToList()

namespace ActiveLifeElderServices.Controllers
{
    // Remove the nested namespace if it's not strictly necessary.
    // It's usually cleaner to have just one namespace per file or logical grouping.
    // namespace ActiveLifeElderServices.Controllers // <--- Consider removing this inner namespace
    // {
    public class ServicesController : Controller
    {
        // GET: /Services/ServiceDetail?serviceName=Medical%20Escort%20%26%20Support
        public IActionResult ServiceDetail(string serviceName)
        {
            // Ensure you're using the main Service model
            if (string.IsNullOrEmpty(serviceName) || !ServiceConstants.ServiceDetails.ContainsKey(serviceName))
            {
                return RedirectToAction("Index", "Services"); // Redirect to the main services page
            }

            var service = ServiceConstants.ServiceDetails[serviceName];
            ViewData["Title"] = service.Name;
            ViewBag.ServiceDetail = service; // Pass the entire Service object to the view
            return View();
        }

        // GET: /Services/Index (General services listing page in a grid view)
        public IActionResult Index()
        {
            ViewData["Title"] = "Our Services";
            // Pass a list of Service objects to the view
            ViewBag.AllServiceDetails = ServiceConstants.ServiceDetails.Values.ToList();
            return View();
        }
    }
    // } // <--- Consider removing this closing brace if you removed the inner namespace
}