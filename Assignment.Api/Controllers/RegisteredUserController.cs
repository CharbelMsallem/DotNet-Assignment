using Assignment.Api.Dtos;
using Assignment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisteredUserController : ControllerBase
    {
        private readonly IRegisteredUserService _registeredUserService;

        public RegisteredUserController(IRegisteredUserService registeredUserService)
        {
            _registeredUserService = registeredUserService;
        }

        // POST: api/RegisteredUser/migrate
        [HttpPost("migrate")]
        public async Task<IActionResult> MigrateUser([FromBody] MigrateRequestDto request)
        {
            var customer = await _registeredUserService.InitiateMigration(request.ICNumber);

            if (customer == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            return Ok(new
            {
                Message = "Migration Initiated",
                MobileNumber = customer.MobileNumber,
                PhoneOtp = customer.PhoneOtp,
                EmailOtp = customer.EmailOtp
            });
        }
    }
}