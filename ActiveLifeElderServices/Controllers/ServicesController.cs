using ActiveLifeElderServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActiveLifeElderServices.Controllers
{
    public class ServicesController : Controller
    {
        // GET: /Services/ServiceDetail?serviceName=Medical%20Escort%20%26%20Support
        public IActionResult ServiceDetail(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName) || !ServiceConstants.ServiceDetails.ContainsKey(serviceName))
            {
                return NotFound(); // Or redirect to a general services page
            }

            ViewData["Title"] = serviceName;
            ViewBag.ServiceDescription = ServiceConstants.ServiceDetails[serviceName];
            ViewBag.ServiceName = serviceName; // Pass the name to the view
            return View();
        }

        // GET: /Services/Index (Optional: A general services listing page)
        public IActionResult Index()
        {
            ViewData["Title"] = "Our Services";
            ViewBag.AllServices = ServiceConstants.AllServiceTypes;
            ViewBag.ServiceDetails = ServiceConstants.ServiceDetails;
            return View();
        }
    }
}
