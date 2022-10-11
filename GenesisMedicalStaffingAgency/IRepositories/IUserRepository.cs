using GenesisMedicalStaffingAgency.Models;

namespace GenesisMedicalStaffingAgency.IRepositories
{
    public interface IUserRepository
    {
        int AddCustomer(User user);
        Tokens AuthenticateUserFromDB(string Email, string Password);
        int? ContactUs(Contact contact);
        int EditCustomer(User user);
        User GetCustomerByEmail(string UserEmail);
        Admin GetAdminByEmail(string UserEmail);
        bool IsTokenValid(string key, string issuer, string token);
    }
}
