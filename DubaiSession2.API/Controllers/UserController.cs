using DubaiSession2.API.Context;
using DubaiSession2.API.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DubaiSession2.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        #region constructor

        private readonly ComXContext _context;
        public UserController()
        {
            _context = new ComXContext();
        }

        #endregion

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody]LoginUserViewModel loginUser)
        {
            if(!ModelState.IsValid) 
                return BadRequest("Please enter valid values for fields.");
            long userId =await _context.Users
                .Where(a => a.Username == loginUser.UserName && a.Password == loginUser.Password)
                .Select(a=>a.Id)
                .FirstOrDefaultAsync();
            if (userId>0)
                return Ok(userId);
            return BadRequest("wrong userName or password");
        }
    }
}
