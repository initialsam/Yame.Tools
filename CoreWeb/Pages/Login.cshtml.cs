using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreWeb.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreWeb
{
    public class LoginModel : PageModel
    {
        public class InputModel
        {
            [Required(ErrorMessage ="{0}是必填欄位")]
            [Display(Name="帳號")]
            public string Username { get; set; }
            [Required(ErrorMessage = "{0}是必填欄位")]
            [Display(Name="密碼")]
            public string Watchword { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }

        [BindProperty]
        public string ErrorMessage { get; set; }

        public ILoginService LoginService { get; }

        public LoginModel(ILoginService loginService)
        {
            LoginService = loginService;
        }
        public void OnGet()
        {
            //ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid == false)
            {

                // If we got this far, something failed, redisplay form
                return Page();
            }

            var isValid = LoginService.IsValidBackendAccount(Input.Username, Input.Watchword);
            if (isValid == false)
            {
                ModelState.AddModelError(string.Empty, $"登入失敗");
                return Page();
            }

            await SignInAsync();

            return LocalRedirect(ReturnUrl);
        }

        private async Task SignInAsync()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Input.Username)
            };

            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(12),
                IsPersistent = true
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authProperties);
        }
    }
}