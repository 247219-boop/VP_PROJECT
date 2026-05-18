using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string username, [FromForm] string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return BadRequest("Username and password cannot be empty.");

            var user = await _userManager.FindByNameAsync(username)
                       ?? await _userManager.FindByEmailAsync(username);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            if (string.IsNullOrEmpty(user.PasswordHash))
                return Unauthorized("This account uses Google Sign-In.");

            var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
                return Ok("Login successful");

            return Unauthorized("Invalid username or password.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] string username, [FromForm] string email, [FromForm] string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return BadRequest("All fields are required.");

            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser != null)
                return BadRequest("Username already exists.");

            existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
                return BadRequest("Email already registered.");

            var user = new IdentityUser
            {
                UserName = username,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
                return Ok("Registration successful");

            return BadRequest(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
