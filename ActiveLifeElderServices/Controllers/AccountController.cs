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
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model) // Make the action async
        {
            if (ModelState.IsValid)
            {
                // --- Backend login logic using EF Core ---
                // 1. Find the user by username
                var user = await _context.Users
                                         .FirstOrDefaultAsync(u => u.Username == model.Username);

                if (user != null)
                {
                    // 2. Verify the password (IMPORTANT: This is where you'd use a password hasher)
                    //    For demonstration, we'll use a simple string comparison.
                    //    In a real app: PasswordHasher.VerifyHashedPassword(user.PasswordHash, model.Password)
                    if (BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                    {
                        // 3. Sign in the user using cookie authentication
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // User ID
                            new Claim(ClaimTypes.Name, user.Username), // Username
                            // You can add more claims here, e.g., roles: new Claim(ClaimTypes.Role, "Admin")
                        };

                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = model.RememberMe // If you add a "Remember Me" checkbox to LoginViewModel
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);

                        return RedirectToAction("Index", "Dashboard"); // Redirect to home or dashboard
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid username or password.");
            }
            // If model state is not valid or login failed
            return View(model);
        }

        // You might also add a Logout action
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        // You could add a Register action here too
        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model) // Now correctly uses RegisterViewModel
        {
            if (ModelState.IsValid)
            {
                // Check if username already exists
                if (await _context.Users.AnyAsync(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("Username", "Username already taken.");
                    return View(model);
                }

                // IMPORTANT: Hash the password before saving!
                // This is a critical security step. DO NOT store plain text passwords.
                // Ensure BCrypt.Net.Core NuGet package is installed: Install-Package BCrypt.Net.Core
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password); // Using BCrypt.Net.Core

                var newUser = new User
                {
                    Username = model.Username,
                    PasswordHash = hashedPassword, // Store the HASHED password
                    Email = model.Email, // Now correctly uses the Email from RegisterViewModel
                    CreatedAt = DateTime.Now
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync(); // Save the new user to the database

                TempData["SuccessMessage"] = "Registration successful! Please log in.";
                return RedirectToAction("Login", "Account"); // Redirect to login page after successful registration
            }
            // If model state is not valid, return to the view with errors
            return View(model);
        }
    
    }
}


