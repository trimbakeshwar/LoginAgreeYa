using LoginAgreeYa.Model;
using LoginAgreeYa.Repository;
using LoginAgreeYa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginAgreeYa.Controllers
{

    [Authorize(Roles ="HR")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        // GET: api/<UserController>
        [HttpGet]
     
        [Route("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            _logger.LogInformation("******** call GetAllUser Api *********");
            return Ok( await _userService.GetAllUser().ConfigureAwait(false));

        }

        // GET api/<UserController>/5
        [HttpGet]

        [Route("GetUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            _logger.LogInformation("******** call GetUser Api *********");
            return Ok(await _userService.GetUser(id).ConfigureAwait(false));
        }

        // POST api/<UserController>
        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] CustomerModel user)
        {
            _logger.LogInformation("******** call AddUser Api *********");
            return Ok(await _userService.AddUser(user).ConfigureAwait(false));
        }

        // PUT api/<UserController>/5
        [HttpPut]
        [Route("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] CustomerModel user)
        {
            _logger.LogInformation("******** call UpdateUser Api *********");
            return Ok(await _userService.UpdateUser(user,id).ConfigureAwait(false));
        }

        // DELETE api/<UserController>/5
        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("******** call DeleteUser Api *********");
            return Ok(await _userService.DeleteUser(id).ConfigureAwait(false));
        }
        [HttpPost]
        [Route("Validating")]
        public IActionResult Validating(string token)
        {
            return Ok( _userService.Validating(token));
        }
        [HttpPost]
        [Route("GenerateToken")]
        public IActionResult GenerateToken(string userEmail)
        {
            return Ok( _userService.GenerateToken(userEmail));
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login (string username, string password)
        {
            return Ok(_userService.Login( username,  password));
        }
    }
}
