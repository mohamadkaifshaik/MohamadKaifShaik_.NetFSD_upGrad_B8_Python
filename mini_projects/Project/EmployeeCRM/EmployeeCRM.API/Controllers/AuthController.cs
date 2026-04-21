using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeCRM.Core.DTOs;
using EmployeeCRM.Core.Interfaces;

namespace EmployeeCRM.API.Controllers
{
    /// <summary>
    /// API Controller for Authentication
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="loginDTO">Email and Password</param>
        /// <returns>JWT Token and User Info</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _authService.LoginAsync(loginDTO);

                if (!result.Success)
                    return Unauthorized(result);

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error during login", error = ex.Message });
            }
        }

        /// <summary>
        /// User Registration
        /// </summary>
        /// <param name="registerDTO">Registration Details</param>
        /// <returns>JWT Token and User Info</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(LoginResponseDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _authService.RegisterAsync(registerDTO);

                if (!result.Success)
                    return BadRequest(result);

                return CreatedAtAction(nameof(Login), result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error during registration", error = ex.Message });
            }
        }
    }
}