using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using taskApi.Data;
using taskApi.Models;
using System.Security.Cryptography;


namespace taskApi.Controllers
{
/*    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public AuthenticationController(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            // Check if the username already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (existingUser != null)
            {
                return Conflict("Username already exists");
            }

            // Hash the password before saving it to the database
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] hashedBytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(user.Password));

                // Convert the byte array to a string representation
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                user.Password = builder.ToString();
            }


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Clear the password before returning the user object
            user.Password = null;

            // For demo purposes, returning the user object (you might want to return a DTO)
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

                if (existingUser == null)
                {
                    return NotFound("Invalid username or password");
                }

                // Hash the provided password for comparison
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] hashedBytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(user.Password));

                    // Convert the byte array to a string representation
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < hashedBytes.Length; i++)
                    {
                        builder.Append(hashedBytes[i].ToString("x2"));
                    }
                    string hashedPassword = builder.ToString();

                    // Compare the hashed password with the stored hashed password
                    if (existingUser.Password != hashedPassword)
                    {
                        return NotFound("Invalid username or password");
                    }
                }

                // Generate JWT token
                var token = GenerateJwtToken(existingUser);

                // For demo purposes, returning the token (you might want to return a DTO containing the token)
                return Ok(new { token });

            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex);
            }
            return Ok();


        }

        private string GenerateJwtToken(User user)
        {
            // Generate a random secret key with sufficient length
            var secretKey = new byte[32]; // 256 bits
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(secretKey);
            }

            var securityKey = new SymmetricSecurityKey(secretKey);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiryInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }*/
}