using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Task_Manager.Data;
using Task_Manager.Infrastructure;
using Task_Manager.Models;

namespace Task_Manager.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult SignIn()
        {
            return View(new UserLoginModel());
        }

        [HttpPost]
        public IActionResult SignIn(UserLoginModel model)
        {
            var user = _context.AppUsers.Include(x => x.AppRole).FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış.");
                return Unauthorized();
            }

            var tokenResponseDto = JwtTokenGenerator.GenerateToken(user);

            Response.Cookies.Append("jwtToken", tokenResponseDto.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            return RedirectToAction("Index", "Task");
        }
    }
}

