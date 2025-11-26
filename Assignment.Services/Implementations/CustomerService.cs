using Assignment.Data;
using Assignment.Data.Entities;
using Assignment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> RegisterRequest(string name, string ic, string phone, string email)
        {
            // ERROR 1: Account Already Exists
            var existingUser = await _context.Customers.FirstOrDefaultAsync(c => c.ICNumber == ic);
            if (existingUser != null)
            {
                // We throw an exception so the Controller returns a 409 Conflict with this message
                throw new Exception("Account already exist. There is account registered with the IC number. Please login to continue.");
            }

            // Create new user
            var newCustomer = new Customer
            {
                CustomerName = name,
                ICNumber = ic,
                MobileNumber = phone,
                Email = email,
                IsPhoneVerified = false,
                IsEmailVerified = false,
                IsPrivacyPolicyAccepted = false,
                BiometricEnabled = false
            };

            GenerateOtps(newCustomer);
            
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();
            return newCustomer;
        }

        public async Task<bool> VerifyPhone(string phone, string code)
        {
            var user = await _context.Customers.FirstOrDefaultAsync(c => c.MobileNumber == phone);
            if (user == null) throw new Exception("User not found.");

            // ERROR 2: Incorrect OTP
            if (user.PhoneOtp != code || user.PhoneOtpExpiry < DateTime.UtcNow)
            {
                throw new Exception("Incorrect OTP. Please enter your OTP again.");
            }

            user.IsPhoneVerified = true;
            user.PhoneOtp = ""; // Clear OTP
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> VerifyEmail(string email, string code)
        {
            var user = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            if (user == null) throw new Exception("User not found.");

            // ERROR 2: Incorrect OTP (Same message for Email)
            if (user.EmailOtp != code || user.EmailOtpExpiry < DateTime.UtcNow)
            {
                throw new Exception("Incorrect OTP. Please enter your OTP again.");
            }

            user.IsEmailVerified = true;
            user.EmailOtp = "";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AcceptPrivacyPolicy(string phone)
        {
            var user = await _context.Customers.FirstOrDefaultAsync(c => c.MobileNumber == phone);
            if (user == null) return false;

            user.IsPrivacyPolicyAccepted = true;
            user.PrivacyPolicyAcceptedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetPin(string phone, string pin, string confirmPin)
        {
            // ERROR 3: Unmatched PIN
            if (pin != confirmPin)
            {
                throw new Exception("Unmatched PIN. Please enter your PIN again.");
            }

            var user = await _context.Customers.FirstOrDefaultAsync(c => c.MobileNumber == phone);
            if (user == null) return false;

            user.Pin = pin;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetBiometric(string phone, bool enabled)
        {
            var user = await _context.Customers.FirstOrDefaultAsync(c => c.MobileNumber == phone);
            if (user == null) return false;

            user.BiometricEnabled = enabled;
            await _context.SaveChangesAsync();
            return true;
        }

        private void GenerateOtps(Customer customer)
        {
            customer.PhoneOtp = new Random().Next(1000, 9999).ToString();
            customer.PhoneOtpExpiry = DateTime.UtcNow.AddMinutes(5);

            customer.EmailOtp = new Random().Next(1000, 9999).ToString();
            customer.EmailOtpExpiry = DateTime.UtcNow.AddMinutes(5);
        }
    }
}