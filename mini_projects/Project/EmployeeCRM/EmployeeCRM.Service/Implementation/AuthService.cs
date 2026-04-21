using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using EmployeeCRM.Core.DTOs;
using EmployeeCRM.Core.Entities;
using EmployeeCRM.Core.Enums;
using EmployeeCRM.Core.Interfaces;

namespace EmployeeCRM.Service.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtSecret;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;
        private readonly int _jwtExpirationMinutes;

        public AuthService(
            IUserRepository userRepository,
            string jwtSecret,
            string jwtIssuer = "EmployeeCRM",
            string jwtAudience = "EmployeeCRMUsers",
            int jwtExpirationMinutes = 60)
        {
            _userRepository = userRepository;
            _jwtSecret = jwtSecret;
            _jwtIssuer = jwtIssuer;
            _jwtAudience = jwtAudience;
            _jwtExpirationMinutes = jwtExpirationMinutes;
        }

        // User login
        public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            try
            {
                // Validation
                if (loginDTO == null)
                    throw new ArgumentNullException(nameof(loginDTO));

                if (string.IsNullOrWhiteSpace(loginDTO.Email) || string.IsNullOrWhiteSpace(loginDTO.Password))
                    throw new ArgumentException("Email and password are required");

                // Get user by email
                var user = await _userRepository.GetByEmailAsync(loginDTO.Email);

                if (user == null)
                    return new LoginResponseDTO
                    {
                        Success = false,
                        Message = "Invalid email or password"
                    };

                // Check if user is active
                if (!user.IsActive)
                    return new LoginResponseDTO
                    {
                        Success = false,
                        Message = "User account is inactive"
                    };

                // Verify password
                if (!_userRepository.VerifyPassword(loginDTO.Password, user.PasswordHash))
                    return new LoginResponseDTO
                    {
                        Success = false,
                        Message = "Invalid email or password"
                    };

                // Update last login date
                user.LastLoginDate = DateTime.Now;
                await _userRepository.UpdateAsync(user);

                // Generate JWT token
                var userDTO = MapToDTO(user);
                var token = GenerateJwtToken(userDTO);

                return new LoginResponseDTO
                {
                    Success = true,
                    Message = "Login successful",
                    Token = token,
                    User = userDTO
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error during login", ex);
            }
        }

        // User registration
        public async Task<LoginResponseDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            try
            {
                // Validation
                if (registerDTO == null)
                    throw new ArgumentNullException(nameof(registerDTO));

                if (string.IsNullOrWhiteSpace(registerDTO.Email))
                    throw new ArgumentException("Email is required");

                if (string.IsNullOrWhiteSpace(registerDTO.FullName))
                    throw new ArgumentException("Full name is required");

                if (string.IsNullOrWhiteSpace(registerDTO.Password))
                    throw new ArgumentException("Password is required");

                if (registerDTO.Password != registerDTO.ConfirmPassword)
                    throw new ArgumentException("Passwords do not match");

                if (registerDTO.Password.Length < 6)
                    throw new ArgumentException("Password must be at least 6 characters");

                // Check if email already exists
                var existingUser = await _userRepository.GetByEmailAsync(registerDTO.Email);
                if (existingUser != null)
                    return new LoginResponseDTO
                    {
                        Success = false,
                        Message = "Email already registered"
                    };

                // Create new user
                var hashedPassword = _userRepository.HashPassword(registerDTO.Password);

                var user = new User
                {
                    Email = registerDTO.Email,
                    FullName = registerDTO.FullName,
                    PasswordHash = hashedPassword,
                    Role = UserRole.Employee, // Default role
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                var result = await _userRepository.AddAsync(user);

                // Generate JWT token
                var userDTO = MapToDTO(result);
                var token = GenerateJwtToken(userDTO);

                return new LoginResponseDTO
                {
                    Success = true,
                    Message = "Registration successful",
                    Token = token,
                    User = userDTO
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error during registration", ex);
            }
        }

        // Generate JWT token
        public string GenerateJwtToken(UserDTO user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSecret);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(_jwtExpirationMinutes),
                    Issuer = _jwtIssuer,
                    Audience = _jwtAudience,
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating JWT token", ex);
            }
        }

        // Helper method: Map Entity to DTO
        private UserDTO MapToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }
    }
}