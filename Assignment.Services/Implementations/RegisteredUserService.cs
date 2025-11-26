using Assignment.Data;
using Assignment.Data.Entities;
using Assignment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Services.Implementations
{
    public class RegisteredUserService : IRegisteredUserService
    {
        private readonly AppDbContext _context;

        public RegisteredUserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> InitiateMigration(string icNumber)
        {
            var user = await _context.Customers.FirstOrDefaultAsync(c => c.ICNumber == icNumber);
            
            if (user == null) return null;

            // 1. Generate NEW Phone OTP
            user.PhoneOtp = new Random().Next(1000, 9999).ToString();
            user.PhoneOtpExpiry = DateTime.UtcNow.AddMinutes(5);

            // 2. Generate NEW Email OTP
            user.EmailOtp = new Random().Next(1000, 9999).ToString();
            user.EmailOtpExpiry = DateTime.UtcNow.AddMinutes(5);

            // 3. RESET ALL FLAGS to FALSE
            user.IsPhoneVerified = false;
            user.IsEmailVerified = false;
            user.IsPrivacyPolicyAccepted = false;
            user.PrivacyPolicyAcceptedAt = null;
            user.BiometricEnabled = false;

            // 4. Wipe old PIN
            user.Pin = string.Empty; 

            await _context.SaveChangesAsync();
            return user;
        }
    }
}