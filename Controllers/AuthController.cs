using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserRegisterDto registerDto)
        {
            // Şifrəni BCrypt ilə standart şəkildə hash-ləyirik
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var user = new User 
            {
                Username = registerDto.Username,
                PasswordHash = passwordHash,
                Email = registerDto.Email
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<object>> Login(UserLoginDto request)
        {
            // İstifadəçini tapırıq
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            
            // İstifadəçi yoxdursa və ya şifrə yanlışdırsa eyni xətanı veririk (təhlükəsizlik üçün)
            if (user == null)
            {
                return BadRequest("İstifadəçi adı və ya şifrə yanlışdır.");
            }

            // Hash-lənmiş şifrəni yoxlayırıq
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            
            if (!isPasswordValid)
            {
                return BadRequest("İstifadəçi adı və ya şifrə yanlışdır.");
            }

            // Giriş uğurludur
            return Ok(new { message = "Giriş uğurludur!" });
        }
    }
}