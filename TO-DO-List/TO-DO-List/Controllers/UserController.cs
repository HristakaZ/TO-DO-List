using DataStructure.Models;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Contracts;
using TO_DO_List.Services.Contracts;
using TO_DO_List.ViewModels;
using TO_DO_List.Attributes;

namespace TO_DO_List.Controllers
{
    public class UserController : Controller
    {
        private readonly IBaseRepository baseRepository;
        private readonly IUserService userService;
        public UserController(IBaseRepository baseRepository,
            IUserService userService)
        {
            this.baseRepository = baseRepository;
            this.userService = userService;
        }

        [Authenticate]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authenticate]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            string? oldPassword = baseRepository.GetAll<User>().FirstOrDefault(x => x.Password
                                                                                == userService.HashPassword(changePasswordViewModel.OldPassword))!
                                                                                .Password;
            if (string.IsNullOrEmpty(oldPassword))
            {
                ModelState.AddModelError("OldPassword", "Please enter a valid password.");
                return View(changePasswordViewModel);
            }

            if (changePasswordViewModel.NewPassword != changePasswordViewModel.ConfirmPassword)
            {
                ModelState.AddModelError("NewPasswordConfirmPassword", "New Password and Confirm Password must match.");
                return View(changePasswordViewModel);
            }

            User loggedInUser = baseRepository.GetAll<User>().FirstOrDefault(x => x.ID == HttpContext.Session.GetInt32("UserID"))!;
            loggedInUser.Password = userService.HashPassword(changePasswordViewModel.NewPassword);
            baseRepository.Update<User>(loggedInUser);

            return RedirectToAction(nameof(ActivityController.Index), "Activity"/*nameof(ActivityController) - this might not work, because this will just stringify the controller's name*/);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            baseRepository.Create<User>(user);

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            string hashedPassword = userService.HashPassword(user.Password);
            //for the next line to be legit, there should be one input that corresponds to both the email and username properties
            User? userFromDb = this.baseRepository.GetAll<User>()
                                                 .FirstOrDefault(x => (x.Username == user.Username || x.Email == user.Email)
                                                                                                   && x.Password == hashedPassword);
            if (userFromDb == null)
            {
                ModelState.AddModelError("LoginError", "A user with this email/username and password does not exist.");
                return View(userFromDb);
            }
            HttpContext.Session.SetInt32("UserID", userFromDb.ID);
            HttpContext.Session.SetString("UserEmail", userFromDb.Email);
            HttpContext.Session.SetString("Username", userFromDb.Username);

            return RedirectToAction(nameof(ActivityController.Index), "Activity"/*nameof(ActivityController) - this might not work, because this will just stringify the controller's name*/);
        }

        [Authenticate]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserID");
            HttpContext.Session.Remove("UserEmail");
            HttpContext.Session.Remove("Username");

            return RedirectToAction(nameof(ActivityController.Index), "Activity"/*nameof(ActivityController) - this might not work, because this will just stringify the controller's name*/);
        }
    }
}
