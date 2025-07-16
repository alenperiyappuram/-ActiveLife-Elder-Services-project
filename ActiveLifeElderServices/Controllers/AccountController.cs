using ActiveLifeElderServices.Data;
using ActiveLifeElderServices.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
 // For LINQ queries like FirstOrDefaultAsync
using System.Threading.Tasks; // For async/await
namespace ActiveLifeElderServices.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            // If the user is already authenticated, redirect them to the dashboard
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Find the user by username (case-insensitive search is good practice)
                var user = await _context.Users
                                         .FirstOrDefaultAsync(u => u.Username.ToLower() == model.Username.ToLower());

                if (user != null)
                {
                    // 2. Verify the password using BCrypt
                    // Ensure BCrypt.Net.BCrypt is used for the Verify method.
                    if (BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                    {
                        // 3. Sign in the user using cookie authentication
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // User ID (e.g., for Appointments)
                            new Claim(ClaimTypes.Name, user.Username), // Username
                            new Claim(ClaimTypes.Email, user.Email) // Add email to claims if available
                            // You can add more claims here, e.g., roles: new Claim(ClaimTypes.Role, "Admin")
                            // new Claim(ClaimTypes.Role, user.Role) // If your User model has a Role property
                        };

                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = model.RememberMe // "Remember Me" functionality
                            // You can also set ExpireTimeSpan here if needed
                            // ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);

                        // Redirect to the return URL if provided, otherwise to Dashboard
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }

                        return RedirectToAction("Index", "Dashboard");
                    }
                }

                // If user not found or password incorrect
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
            }
            // If model state is not valid or login failed
            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // Optionally clear TempData or Session if needed
            TempData["SuccessMessage"] = "You have been logged out.";
            return RedirectToAction("Login", "Account");
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            // If the user is already authenticated, redirect them away from the register page
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if username already exists
                if (await _context.Users.AnyAsync(u => u.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError("Username", "Username already taken. Please choose a different username.");
                    return View(model);
                }

                // Check if email already exists (optional, but good practice)
                if (await _context.Users.AnyAsync(u => u.Email.ToLower() == model.Email.ToLower()))
                {
                    ModelState.AddModelError("Email", "Email address is already registered.");
                    return View(model);
                }

                // IMPORTANT: Hash the password before saving!
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

                var newUser = new User
                {
                    Username = model.Username,
                    PasswordHash = hashedPassword, // Store the HASHED password
                    Email = model.Email,
                    CreatedAt = DateTime.Now
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync(); // Save the new user to the database

                TempData["SuccessMessage"] = "Registration successful! You can now log in.";
                return RedirectToAction("Login", "Account"); // Redirect to login page after successful registration
            }
            // If model state is not valid, return to the view with errors
            return View(model);
        }
    }
}    



