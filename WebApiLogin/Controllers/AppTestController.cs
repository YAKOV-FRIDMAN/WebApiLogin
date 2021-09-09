using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiLogin.Model;
using WebApiLogin.Service;

namespace WebApiLogin.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AppTestController : ControllerBase
    {
        IMyLogin myLogin;
        public AppTestController(IMyLogin myLogin)
        {
            this.myLogin = myLogin;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(MyUser user)
        {
           string token =  myLogin.Login(user.User, user.Password);
            if (token == null)
            {
                return Unauthorized("not user");

            }
            return Ok(token);
        }



        [AllowAnonymous]
        [HttpPost("Loginn")]
        public async Task<IActionResult> Loginn(MyUser user)
        {
            if (myLogin.Login(user.User, user.Password) == null)
                return BadRequest();

          //  var user = GetUserFromUsername(username);

            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "Admin"),
                //...
            }, "Cookies");

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await Request.HttpContext.SignInAsync("Cookies", claimsPrincipal);
            return Ok();
            //return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return NoContent();
        }
    }
}
