using ActiveLifeElderServices.Data;
using ActiveLifeElderServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

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
     
        private void PopulateViewBags()
        {
            ViewBag.ServiceTypes = ServiceConstants.AllServiceTypes
                                    .Select(s => new SelectListItem { Value = s, Text = s })
                                    .ToList();
            // Place breakpoint here:
            ViewBag.CaregiverGenderOptions = ServiceConstants.CaregiverGenderOptions;
        }


        // GET: /Appointment/Book
        // Displays the appointment booking form
        public IActionResult Book()
        {
            PopulateViewBags(); // Populate for GET request


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
            if (ServiceConstants.GenderApplicableServices.Contains(model.SelectedServiceType) && string.IsNullOrEmpty(model.CaregiverGenderPreference))
            {
                ModelState.AddModelError("CaregiverGenderPreference", "Gender preference is required for this service type.");
            }

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.CaregiverGenderPreference))
                {
                    model.CaregiverGenderPreference = "No Preference";
                }

                // Map data from ViewModel to your Appointment entity
                var newAppointment = new Appointment
                {
                    ClientName = model.ClientName,
                    Address = model.Address,
                    AppointmentDate = model.AppointmentDate,
                    AppointmentTime = model.AppointmentTime,
                    Email = model.Email,
                    NearestLandmark = model.NearestLandmark,
                    ServiceType = model.SelectedServiceType, // This is where the selected value should be
                    CreatedAt = DateTime.Now,
                    ContactAddress=model.ContactAddress,
                    CaregiverGenderPreference=model.CaregiverGenderPreference,
                    ContactPhoneNumber=model.ContactPhoneNumber,
                    Status = "Pending",
                    AlternatePhoneNumber=model.AlternatePhoneNumber,
                    AdditionalRequirements=model.AdditionalRequirements
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
            PopulateViewBags();
            return View(model); // Return the view with the model (including errors)
        }
        public IActionResult ListAppointments()
        {
            ViewData["Title"] = "My Upcoming Appointments";

            List<Appointment> appointments = new List<Appointment>();

            if (User.Identity.IsAuthenticated && int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int currentUserId))
            {
                // Combine date and time for comparison
                var now = DateTime.Now;

                appointments = _context.Appointments
                                  .Where(a => a.UserId == currentUserId &&
                                              (a.AppointmentDate.Date > now.Date || // Date is in the future
                                               (a.AppointmentDate.Date == now.Date && // Date is today AND
                                                (a.AppointmentTime.Hour > now.Hour || // Hour is in the future OR
                                                 (a.AppointmentTime.Hour == now.Hour && a.AppointmentTime.Minute >= now.Minute))))) // Hour is current AND Minute is current or future
                                  .OrderBy(a => a.AppointmentDate)
                                  .ThenBy(a => a.AppointmentTime)
                                  .ToList();
            }
            else
            {
                // If user is not authenticated, return an empty list or redirect to login
                // For now, an empty list is fine.
            }

            return View(appointments);
        }
    
    }
    // You might add other actions here later, e.g., ViewAppointments, EditAppointment, etc.
}

