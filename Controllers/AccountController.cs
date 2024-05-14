
using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using webtintuc.Models;
using webtintuc.Account.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace webtintuc.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IEmailSender emailSender;

        //constructor
        public AccountController(SignInManager<AppUser> _signInManager, UserManager<AppUser> _userManager, IEmailSender _emailSender)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            emailSender = _emailSender;
        }

        //GET:đăng nhập
        public IActionResult Login()
        {
            return View();

        }

        //POST:đăng nhập
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(login.UserName!, login.Password!, login.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(login.UserName!);
                    if (await userManager.IsInRoleAsync(user!, "Admin"))
                    {
                        return RedirectToAction("Index", "AdminCP");
                    }
                    else
                    {
                        return NotFound("Không đủ quyền truy cập!");
                    }
                }
                ModelState.AddModelError("", "Đăng nhập thất bại!");
            }
            return View(login);

        }

        //GET: Đăng ký
        public IActionResult Register(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        //POST: Đăng ký
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Register(RegisterModel registerModel, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = registerModel.Name,
                    Email = registerModel.Email,
                };
                var result = await userManager.CreateAsync(user, registerModel.Password!);
                if (result.Succeeded)
                {
                    //khong xac thuc email:

                    /*await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");*/


                    // Xac thuc email:

                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user); // Phát sinh token để xác nhận email
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    // https://localhost:5001/confirm-email?userId=fdsfds&code=xyz&returnUrl=
                    var callbackUrl = Url.ActionLink(
                        action: nameof(ConfirmEmail),
                        values:
                            new
                            {
                                userId = user.Id,
                                code = code
                            },
                        protocol: Request.Scheme);

                    await emailSender.SendEmailAsync(registerModel.Email!,
                        "Xác nhận địa chỉ email",
                        @$"Bạn vừa đăng ký tài khoản trên trên webtintuc, 
                     Hãy <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>bấm vào đây</a> 
                     để kích hoạt tài khoản.");

                    if (userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return LocalRedirect(Url.Action(nameof(RegisterConfirmation)));
                    }
                    else
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }

                }
                foreach (var erro in result.Errors)
                {
                    ModelState.AddModelError("", erro.Description);
                }

            }
            return View(registerModel);
        }
        // GET: xác nhận email
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterConfirmation()
        {
            return View();
        }

        // GET: XÁC NHẬN EMAIL:
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("ErrorConfirmEmail");
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("ErrorConfirmEmail");
            }
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "ErrorConfirmEmail");
        }

        // GET: /Account/ForgotPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        //GET:Quên mật khẩu:
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // POST: /Account/ForgotPassword :QUên mật khẩu
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                var check = await userManager.IsEmailConfirmedAsync(user);
                if (user == null || !check)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }
                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.ActionLink(
                    action: nameof(ResetPassword),
                    values: new { code },
                    protocol: Request.Scheme);


                await emailSender.SendEmailAsync(
                    model.Email,
                    "Reset Password",
                    $"Hãy bấm <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>vào đây</a> để đặt lại mật khẩu.");

                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }
            return View(model);
        }

        // GET: /Account/ResetPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        //
        // GET: /Account/ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.FindByEmailAsync(model.Email!);
            if (user == null)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }
            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Code!));

            var result = await userManager.ResetPasswordAsync(user, code, model.Password!);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }
            foreach (var erro in result.Errors)
            {
                ModelState.AddModelError("", erro.Description);
            }
            return View();
        }
        //Đăng xuất:
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }

}