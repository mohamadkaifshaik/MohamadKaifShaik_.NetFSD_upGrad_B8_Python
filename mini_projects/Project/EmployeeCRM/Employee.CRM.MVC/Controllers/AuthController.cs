using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using EmployeeCRM.Core.DTOs;
using EmployeeCRM.MVC.Services;

namespace EmployeeCRM.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthApiClient _authApiClient;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthApiClient authApiClient, ILogger<AuthController> logger)
        {
            _authApiClient = authApiClient;
            _logger = logger;
        }

        // Display Login View
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard");

            return View();
        }

        // Handle Login Form Submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return View(loginDTO);

            try
            {
                var response = await _authApiClient.LoginAsync(loginDTO);

                if (!response.Success)
                {
                    ModelState.AddModelError("", response.Message);
                    return View(loginDTO);
                }

                // Create claims for the user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, response.User.Id.ToString()),
                    new Claim(ClaimTypes.Email, response.User.Email),
                    new Claim(ClaimTypes.Name, response.User.FullName),
                    new Claim(ClaimTypes.Role, response.User.Role.ToString()),
                    new Claim("Token", response.Token)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                // Store JWT token in session
                HttpContext.Session.SetString("JwtToken", response.Token);

                _logger.LogInformation($"User {response.User.Email} logged in successfully");

                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Login error: {ex.Message}");
                ModelState.AddModelError("", "An error occurred during login. Please try again.");
                return View(loginDTO);
            }
        }

        // Display Register View
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard");

            return View();
        }

        // Handle Register Form Submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return View(registerDTO);

            try
            {
                var response = await _authApiClient.RegisterAsync(registerDTO);

                if (!response.Success)
                {
                    ModelState.AddModelError("", response.Message ?? "Unknown error");
                    Console.WriteLine(response.Message);
                    return View(registerDTO);
                }

                // Auto login after registration
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, response.User.Id.ToString()),
                    new Claim(ClaimTypes.Email, response.User.Email),
                    new Claim(ClaimTypes.Name, response.User.FullName),
                    new Claim(ClaimTypes.Role, response.User.Role.ToString()),
                    new Claim("Token", response.Token)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                HttpContext.Session.SetString("JwtToken", response.Token);

                _logger.LogInformation($"User {response.User.Email} registered and logged in successfully");

                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Registration error: {ex.Message}");
                ModelState.AddModelError("", "An error occurred during registration. Please try again.");
                return View(registerDTO);
            }
        }

        // Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("JwtToken");

            _logger.LogInformation($"User logged out");

            return RedirectToAction("Index", "Home");
        }
    }
}