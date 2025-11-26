using System.ComponentModel.DataAnnotations;

namespace Assignment.Api.Dtos
{
    // Step 1: Initial Register
    public class RegisterRequestDto
    {
        [Required]
        public string CustomerName { get; set; } = string.Empty;
        
        [Required]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "IC Number must be exactly 12 digits.")]
        public string ICNumber { get; set; } = string.Empty;
        
        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Mobile Number must be exactly 8 digits.")]
        public string MobileNumber { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }

    public class MigrateRequestDto
    {
        [Required]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "IC Number must be exactly 12 digits.")]
        public string ICNumber { get; set; } = string.Empty;
    }

    // Step 2 & 3: Verify OTP
    public class VerifyOtpDto
    {
        [Required]
        public string Identifier { get; set; } = string.Empty; 
        [Required]
        public string Code { get; set; } = string.Empty;
    }

    // Step 4: Privacy
    public class PrivacyDto
    {
        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Mobile Number must be exactly 8 digits.")]
        public string MobileNumber { get; set; } = string.Empty;
    }

    // Step 5: Set PIN
    public class SetPinDto
    {
        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Mobile Number must be exactly 8 digits.")]
        public string MobileNumber { get; set; } = string.Empty;
        
        [Required]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "PIN must be exactly 6 digits.")]
        public string Pin { get; set; } = string.Empty;
        
        [Required]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Confirm PIN must be exactly 6 digits.")]
        public string ConfirmPin { get; set; } = string.Empty;
    }

    // Step 6: Biometrics
    public class BiometricDto
    {
        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Mobile Number must be exactly 8 digits.")]
        public string MobileNumber { get; set; } = string.Empty;
        public bool Enabled { get; set; }
    }
}