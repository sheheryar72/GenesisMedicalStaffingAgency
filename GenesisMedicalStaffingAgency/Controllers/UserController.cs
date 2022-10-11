using GenesisMedicalStaffingAgency.IRepositories;
using GenesisMedicalStaffingAgency.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenesisMedicalStaffingAgency.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("IsUserLoggedIn") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult AuthenticateUser(string Email, string Password)
        {
            var token = _userRepository.AuthenticateUserFromDB(Email, Password);
            if (token != null)
            {
                HttpContext.Session.SetString("IsUserLoggedIn", token.Token.ToString());
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "User");
        }
        public IActionResult InsertContact(Contact contact)
        {
            int? id = _userRepository.ContactUs(contact);
            if(id != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Contact", "Home");
        }
    }
}
