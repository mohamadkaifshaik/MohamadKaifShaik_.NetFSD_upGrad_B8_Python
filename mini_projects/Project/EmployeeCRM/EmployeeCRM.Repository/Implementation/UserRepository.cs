using System;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EmployeeCRM.Core.Entities;
using EmployeeCRM.Core.Interfaces;
using EmployeeCRM.Data;

namespace EmployeeCRM.Repository.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        // Get user by email
        public async Task<User> GetByEmailAsync(string email)
        {
            try
            {
                return await _dbSet
                    .FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving user by email", ex);
            }
        }

        // Verify password against hash
        public bool VerifyPassword(string password, string hash)
        {
            try
            {
                // Using SHA256 for demonstration
                // In production, use more secure methods like bcrypt
                using (var sha256 = SHA256.Create())
                {
                    var hashedInput = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    var hashBytes = Convert.FromBase64String(hash);

                    return hashedInput.SequenceEqual(hashBytes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error verifying password", ex);
            }
        }

        // Hash password
        public string HashPassword(string password)
        {
            try
            {
                // Using SHA256 for demonstration
                // In production, use more secure methods like bcrypt
                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return Convert.ToBase64String(hashedBytes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error hashing password", ex);
            }
        }
    }
}