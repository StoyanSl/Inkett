using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Inkett.Web.Models.InputModels.Account;
using Inkett.Infrastructure.Identity;
using Inkett.ApplicationCore.Interfaces.Services;
using System.Security.Claims;

namespace Inkett.Web.Controllers
{
    public class AccountController:Controller
    {
        private readonly InkettUserManager _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IProfileService _profileService;
        

        public AccountController(
            InkettUserManager userManager,
            SignInManager<ApplicationUser> signInManager,
            IProfileService profileService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _profileService = profileService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index","Home");
            }
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ViewData["ReturnUrl"] = returnUrl;
            ApplicationUser signedUser = await _userManager.FindByEmailAsync(model.Email);
            if (signedUser!=null)
            {
                var result = await _signInManager.PasswordSignInAsync(signedUser, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
            }
            
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(ProfileController.Index), "Home");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Email = model.Email, UserName=model.Email};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                   var profile= await _profileService.CreateProfileAsync(user.Id,model.ProfileName,String.Empty);
                    await _userManager.AddClaimAsync(user, new Claim("ProfileId", profile.Id.ToString()));
                    await _signInManager.SignInAsync(user, isPersistent: true);

                    return RedirectToAction("Index","Profile");
                }
                AddErrors(result);
            }
            return View(model);
        }
        

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (returnUrl is null)
            {
               return RedirectToAction(nameof(ProfileController.Index), "Home");
            }
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(ProfileController.Index), "Home");
            }
        }

        private void AddErrors(IdentityResult result)
        {
            
            foreach (var error in result.Errors)
            {
                if (error.Code== "DuplicateUserName")
                {
                  error.Description=  error.Description.Replace("User name", "");
                }
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
