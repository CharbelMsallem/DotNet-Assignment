using Assignment.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Services.Interfaces
{
    public interface ICustomerService
    {
        // Step 1: Register Customer
        Task<Customer> RegisterRequest(string name, string ic, string phone, string email);
        
        // Step 2: Verify Phone
        Task<bool> VerifyPhone(string phone, string code);

        // Step 3: Verify Email
        Task<bool> VerifyEmail(string email, string code);

        // Step 4: Accept Privacy Policy
        Task<bool> AcceptPrivacyPolicy(string phone);

        // Step 5: Set PIN (Added confirmPin)
        Task<bool> SetPin(string phone, string pin, string confirmPin);

        // Step 6: Enable Biometrics (Optional final step)
        Task<bool> SetBiometric(string phone, bool enabled);
    }
}