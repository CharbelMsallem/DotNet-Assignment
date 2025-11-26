using System.ComponentModel.DataAnnotations;

namespace Assignment.Api.Dtos
{
    // Step 1: Initial Register
    public class RegisterRequestDto
    {
        [Required]
        public string CustomerName { get; set; } = string.Empty;
        [Required]
        public string ICNumber { get; set; } = string.Empty;
        [Required]
        public string MobileNumber { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }

    // Step 2 & 3: Verify OTP
    public class VerifyOtpDto
    {
        [Required]
        public string Identifier { get; set; } = string.Empty; // Phone or Email
        [Required]
        public string Code { get; set; } = string.Empty;
    }

    // Step 4: Privacy
    public class PrivacyDto
    {
        [Required]
        public string MobileNumber { get; set; } = string.Empty;
    }

    // Step 5: Set PIN
    public class SetPinDto
    {
        [Required]
        public string MobileNumber { get; set; } = string.Empty;
        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string Pin { get; set; } = string.Empty;
        [Required]
        public string ConfirmPin { get; set; } = string.Empty;
    }

    // Step 6: Biometrics
    public class BiometricDto
    {
        [Required]
        public string MobileNumber { get; set; } = string.Empty;
        public bool Enabled { get; set; }
    }
}