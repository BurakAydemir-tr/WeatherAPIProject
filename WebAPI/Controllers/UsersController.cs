using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/Login")]
        public IActionResult Login(string email, string password)
        {
            var result=_userService.Login(email, password);
            if (result!=null)
            {
                return Ok(result);
            }
            return BadRequest("Hatalı email yada şifre girişi yaptınız.");
        }
    }
}
