using System.ComponentModel.DataAnnotations;

namespace Assignment.Data.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        // ----------- Profile Data -----------
        [Required]
        public string CustomerName { get; set; } = string.Empty; 

        [Required]
        public string ICNumber { get; set; } = string.Empty; 

        [Required]
        public string MobileNumber { get; set; } = string.Empty; 

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;


        // ----------- Phone Verification Logic -----------
        public string PhoneOtp { get; set; } = string.Empty;
        public DateTime PhoneOtpExpiry { get; set; }
        public bool IsPhoneVerified { get; set; } = false;


        // ----------- Email Verification Logic -----------
        public string EmailOtp { get; set; } = string.Empty;
        public DateTime EmailOtpExpiry { get; set; }
        public bool IsEmailVerified { get; set; } = false;

        // ----------- Privacy Policy -----------
        public bool IsPrivacyPolicyAccepted { get; set; } = false;
        public DateTime? PrivacyPolicyAcceptedAt { get; set; }


        // ----------- Security Setup -----------
        [StringLength(6)]
        public string Pin { get; set; } = string.Empty; // Stores the 6-digit PIN
        public bool BiometricEnabled { get; set; } = false;

    }
}