using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Verification.Model;
using Verification.Services;

namespace Verification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserOperation _userOperation;

        public UsersController(IUserOperation userOperation)
        {
            _userOperation = userOperation;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUser()
        {
            return _userOperation.GetUser();
        }
        // POST: api/Users
        [HttpPost]
        public  IActionResult PostUser([FromBody] User value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _userOperation.AddUser(value);
            return Ok(value);
        }
        [HttpPost("verify")]
        public async Task<IActionResult> PostVerify([FromBody] string value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userOperation.Verify(value);
            if(user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
    }
}
