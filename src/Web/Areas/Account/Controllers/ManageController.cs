using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Inkett.Infrastructure.Identity;
using Inkett.Web.Areas.Account.ViewModels;

namespace Inkett.Web.Areas.Account.Controllers
{
    [Authorize]
    [Area("Account")]
    public class ManageController : Controller
    {
        private readonly InkettUserManager _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UrlEncoder _urlEncoder;


        public ManageController(
          InkettUserManager userManager,
          SignInManager<ApplicationUser> signInManager,
          UrlEncoder urlEncoder)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _urlEncoder = urlEncoder;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new IndexViewModel
            {
                Email = user.Email,
                FirstName=user.FirstName,
                LastName=user.LastName,
                BirthdayDate=user.BirthdayDate??DateTime.UtcNow,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var email = user.Email;
            if (model.Email != email)
            {
               var result =  await _userManager.SetUserNameAsync(user, model.Email);
                if (!result.Succeeded)
                {
                    AddErrors(result);
                }
                else
                {
                    await _userManager.SetEmailAsync(user, model.Email);
                }
            }

            var firstName = user.FirstName;
            if (model.FirstName != firstName)
            {
                await _userManager.SetUserFirstNameAsync(user,model.FirstName);
                
            }
            var lastName = user.LastName;
            if (model.LastName != lastName)
            {
                await _userManager.SetUserLastNameAsync(user, model.LastName);

            }
            var birthdate = user.BirthdayDate;
            if (model.BirthdayDate != birthdate)
            {
                await _userManager.SetUserBirthDayAsync(user, model.BirthdayDate);
            }
            if (ModelState.ErrorCount==0)
            {
                StatusMessage = "Your profile has been updated";
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "Your password has been changed.";

            return RedirectToAction(nameof(ChangePassword));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                if (error.Code == "DuplicateUserName")
                {
                    error.Description = error.Description.Replace("User name", "");
                }
                ModelState.AddModelError("", error.Description);
            }
        }

        
    }
}
