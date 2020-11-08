using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet("[action]")]
        public IActionResult Login()
        {

            return Created("", new TokenGenerator().Generate());
        }
        [HttpGet("[action]")]
        public IActionResult AdminLogin()
        {

            return Created("", new TokenGenerator().AdminTokenGenerate());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult AdminSayfasi()
        {
            var sehir =User.Claims.Where(I => I.Type == "City").FirstOrDefault(); // özel olarak tanımladığımız claimi çağırmak için kullanırız. bize liste döner
            var userName = User.Identity.Name; //Token hangi kullanıcıya ait diye kontrol ederiz.
            return Ok("Token geçti. Kullanıcı:" + userName);
        }
        [Authorize]
        [HttpGet("[action]")]
        public IActionResult Erisim()
        {
            return Ok("Token geçti");
        }
    }
}
