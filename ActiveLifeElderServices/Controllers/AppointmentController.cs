using ActiveLifeElderServices.Data;
using ActiveLifeElderServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ActiveLifeElderServices.Controllers
{
    [Authorize] // Ensure only logged-in users can book appointments
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Helper method to populate ViewBag.ServiceTypes
        private void PopulateServiceTypes()
        {
            ViewBag.ServiceTypes = ServiceConstants.AllServiceTypes
                                    .Select(s => new SelectListItem { Value = s, Text = s })
                                    .ToList();
        }

        // GET: /Appointment/Book
        // Displays the appointment booking form
        public IActionResult Book()
        {
            PopulateServiceTypes(); // Populate for GET request

            var viewModel = new BookAppointmentViewModel();

            // Optional: Pre-fill email and client name if user is logged in
            if (User.Identity.IsAuthenticated)
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == User.Identity.Name);
                if (user != null)
                {
                    viewModel.Email = user.Email;
                    viewModel.ClientName = user.Username;
                }
            }

            return View(viewModel);
        }

        // POST: /Appointment/Book
        // Processes the submitted appointment booking form
        [HttpPost]
        [ValidateAntiForgeryToken] // Essential for security
        public async Task<IActionResult> Book(BookAppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map data from ViewModel to your Appointment entity
                var newAppointment = new Appointment
                {
                    ClientName = model.ClientName,
                    Address = model.Address,
                    AppointmentDateTime = model.AppointmentDateTime,
                    Email = model.Email,
                    NearestLandmark = model.NearestLandmark,
                    ServiceType = model.SelectedServiceType, // This is where the selected value should be
                    CreatedAt = DateTime.Now,
                    Status = "Pending"
                };

                // Link the appointment to the logged-in user
                if (User.Identity.IsAuthenticated && int.TryParse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out int userId))
                {
                    newAppointment.UserId = userId;
                }

                _context.Appointments.Add(newAppointment);
                await _context.SaveChangesAsync(); // Save the new appointment to the database

                TempData["SuccessMessage"] = "Your appointment request has been successfully submitted! We will contact you shortly.";
                return RedirectToAction("Index", "Dashboard"); // Redirect to dashboard or a confirmation page
            }

            // CRUCIAL: If ModelState is NOT valid, re-populate ViewBag.ServiceTypes
            // before returning the view, otherwise the dropdown will be empty on re-render.
            PopulateServiceTypes();
            return View(model); // Return the view with the model (including errors)
        }
    }
    // You might add other actions here later, e.g., ViewAppointments, EditAppointment, etc.
}

