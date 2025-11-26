using Assignment.Data.Entities;

namespace Assignment.Services.Interfaces
{
    public interface IRegisteredUserService
    {
        // finding the user by IC and generating the new OTPs
        Task<Customer?> InitiateMigration(string icNumber);
    }
}