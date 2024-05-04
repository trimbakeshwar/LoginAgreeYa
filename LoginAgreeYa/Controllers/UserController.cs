using LoginAgreeYa.Model;
using LoginAgreeYa.Repository;
using LoginAgreeYa.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginAgreeYa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<UserController>
        [HttpGet]
     
        [Route("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok( await _userService.GetAllUser().ConfigureAwait(false));
        }

        // GET api/<UserController>/5
        [HttpGet]

        [Route("GetUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(await _userService.GetUser(id).ConfigureAwait(false));
        }

        // POST api/<UserController>
        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] RegistrationModel user)
        {
            return Ok(await _userService.AddUser(user).ConfigureAwait(false));
        }

        // PUT api/<UserController>/5
        [HttpPut]
        [Route("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] RegistrationModel user)
        {
            return Ok(await _userService.UpdateUser(user,id).ConfigureAwait(false));
        }

        // DELETE api/<UserController>/5
        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok(await _userService.DeleteUser(id).ConfigureAwait(false));
        }
    }
}
