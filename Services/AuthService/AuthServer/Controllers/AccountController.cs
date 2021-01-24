using AuthServer.Infrastructure;
using AuthServer.Infrastructure.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthServer.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Route("account/SignInWithGoogle")]
        public IActionResult SignInWithGoogle()
        {
            var authenticationProperties = _signInManager.ConfigureExternalAuthenticationProperties("Google", Url.Action(nameof(HandleExternalLogin)));
            return Challenge(authenticationProperties, "Google");
        }

        [Route("account/HandleExternalLogin")]
        public async Task<IActionResult> HandleExternalLogin()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (!result.Succeeded) //user does not exist yet
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var name = info.Principal.FindFirstValue("name");
                var newUser = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name,
                    UserName = email,
                    Email = email,
                    LoginType = info.LoginProvider,
                    EmailConfirmed = true
                };
                var createResult = await _userManager.CreateAsync(newUser);
                if (!createResult.Succeeded)
                    throw new Exception(createResult.Errors.Select(e => e.Description).Aggregate((errors, error) => $"{errors}, {error}"));

                await _userManager.AddLoginAsync(newUser, info);
                var newUserClaims = info.Principal.Claims.Append(new Claim("userId", newUser.Id)).Append(new Claim("role", Roles.Consumer));
                await _userManager.AddClaimsAsync(newUser, newUserClaims);

                await _signInManager.SignInAsync(newUser, isPersistent: false);
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            }
            //else
            //{
            //    var token = info.Principal.Claims.FirstOrDefault(x => x.Type == "token");
            //    if (token != null)
            //    {

            //    }
            //}
            string redirectUrl = $"http://localhost:4200?token={info.AuthenticationTokens.FirstOrDefault(token => token.Name == "id_token").Value}";

            return Redirect(redirectUrl);
        }

        [Route("account/isAuthenticated")]
        public IActionResult IsAuthenticated()
        {
            var s = _userManager.GetAuthenticationTokenAsync(_userManager.GetUserAsync(User).Result, "Google", "id_token").Result;
            // add code to verify token expiry here
            return new ObjectResult(User.Identity.IsAuthenticated);
        }

        [Route("account/logout")]
        public async Task<bool> Logout()
        {
            await _signInManager.SignOutAsync();
            return true;
        }
    }
}
