using Assignment.Api.Dtos;
using Assignment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public RegistrationController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // 1. REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            try
            {
                var customer = await _customerService.RegisterRequest(
                    request.CustomerName, 
                    request.ICNumber, 
                    request.MobileNumber, 
                    request.Email);

                // MOCK: Returning the OTPs in response so we can test immediately
                return Ok(new 
                { 
                    Message = "Registration Initiated", 
                    PhoneOtp = customer.PhoneOtp, 
                    EmailOtp = customer.EmailOtp 
                });
            }
            catch (Exception ex)
            {
                // Returns 409 Conflict for "Account already exist"
                return Conflict(new { Message = ex.Message });
            }
        }

        // 2. VERIFY PHONE
        [HttpPost("verify-phone")]
        public async Task<IActionResult> VerifyPhone([FromBody] VerifyOtpDto request)
        {
            try
            {
                await _customerService.VerifyPhone(request.Identifier, request.Code);
                return Ok(new { Message = "Phone Verified Success" });
            }
            catch (Exception ex)
            {
                // Returns 400 Bad Request for "Incorrect OTP"
                return BadRequest(new { Message = ex.Message });
            }
        }

        // 3. VERIFY EMAIL
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyOtpDto request)
        {
            try
            {
                await _customerService.VerifyEmail(request.Identifier, request.Code);
                return Ok(new { Message = "Email Verified Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // 4. PRIVACY POLICY
        [HttpPost("accept-privacy")]
        public async Task<IActionResult> AcceptPrivacy([FromBody] PrivacyDto request)
        {
            var result = await _customerService.AcceptPrivacyPolicy(request.MobileNumber);
            if (!result) return NotFound("User not found");
            return Ok(new { Message = "Privacy Policy Accepted" });
        }

        // 5. SET PIN
        [HttpPost("set-pin")]
        public async Task<IActionResult> SetPin([FromBody] SetPinDto request)
        {
            try
            {
                var success = await _customerService.SetPin(request.MobileNumber, request.Pin, request.ConfirmPin);
                
                if (!success) 
                {
                    return NotFound(new { Message = "User not found" });
                }

                return Ok(new { Message = "PIN Set Successfully" });
            }
            catch (Exception ex)
            {
                // Returns 400 Bad Request for "Unmatched PIN"
                return BadRequest(new { Message = ex.Message });
            }
        }

        // 6. BIOMETRICS
        [HttpPost("set-biometric")]
        public async Task<IActionResult> SetBiometric([FromBody] BiometricDto request)
        {
            var success = await _customerService.SetBiometric(request.MobileNumber, request.Enabled);
            
            // If service returns false (user not found), return 404
            if (!success) 
            {
                return NotFound(new { Message = "User not found" });
            }

            return Ok(new { Message = "Biometrics Updated" });
        }
    }
}