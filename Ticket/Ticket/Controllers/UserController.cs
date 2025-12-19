using Helper.VieModels;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Interfaces;

namespace Ticket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserViewModel item)
        {
            var res = await _service.RegisterAsync(item);
            return Ok(res);
        }
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginRequestViewModel item)
        {
            var res = await _service.LoginAsync(item);
            return Ok(res);
        }
    }
}
