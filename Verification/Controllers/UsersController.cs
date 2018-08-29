using System.Collections.Generic;
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
        public  void PostUser([FromBody] string value)
        {
            _userOperation.AddUser(value);
        }
        [HttpPost("verify")]
        public async void PostVerify([FromBody] string value)
        {
            await _userOperation.Verify(value);
        }
    }
}
