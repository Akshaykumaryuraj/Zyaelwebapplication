using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Protocol.Plugins;
using System.Security.Claims;
using ZyaelWeb_Models.Logins;
using ZyaelWeb_Services.Logins;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ZyaelWeb.Controllers.Logins
{
    public class AdminLoginController : Controller

    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IHostingEnvironment _hostingEnvironment;
        public AdminLogin _adminlogin;


        public AdminLoginController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _adminlogin = new AdminLogin(httpContextAccessor, config);
        }

        [HttpPost]
        public async Task<IActionResult> SetAdminLogin(AdminLoginModel item)
        {
             AdminLoginModel result = new AdminLoginModel();
            result = await _adminlogin.SetAdminLogin(item);

            if (result.returnId != -1)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Actor,Convert.ToString(result.AdminUserID)),
                    //to avoid error
                    new Claim(ClaimTypes.Sid,Convert.ToString(result.VendorUserID)),
                    new Claim(ClaimTypes.Name,Convert.ToString(result.Role)),

                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, "Zyael");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                await HttpContext.SignInAsync(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                    principal, props);
                return RedirectToAction("AdminDashBoard", "Admin", result);
      

            }
            else
            {
                TempData["ErrorMessage"] = "Invalid Credentials";
                return RedirectToAction("Index", "Login");
                //return Json("unsuccessful");
                //return Json(result.returnId);



            }

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminLogin()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            return View();
        }

    }

}

