using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Account.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        
        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Model.Account account)
        {
            //Console.WriteLine("login" + account.UserName);

            var user = await _userManager.FindByNameAsync(account.UserName);
            
            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, account.Password, true, false);

                if (signInResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(signInResult.ToString());
                }
            }
            else
            {
                return BadRequest("用户没有登录");
            }
        }
        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Model.Account account)
        {
            Console.WriteLine(account.UserName);
            var user = new IdentityUser
            {
                UserName = account.UserName,
                Email = ""
            };
            var rst = await _userManager.CreateAsync(user, account.Password);

            if (rst.Succeeded)
            {
                return CreatedAtAction(nameof(Register), user);
                //Todo
                return CreatedAtAction(nameof(Register),new {id = user.Id},new
                {
                    id = user.Id,
                    name = user.UserName
                });
            }
            else
            {
                var sb = new StringBuilder();
                foreach (var error in rst.Errors)
                    sb.AppendLine($"{error.Code}:{error.Description}");
                return BadRequest(rst.ToString());
            }
        }
        
        [HttpGet]
        public ActionResult GetDishes()
        {
            string[] recipes = { "Oxtail", "Curry Chicken", "Dumplings"};
            if (recipes.Any())
                return NotFound();
            return Ok(recipes);
        }
        
    }
}