using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZyaelWeb_Models.Logins;
using ZyaelWeb_Services.Logins;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ZyaelWeb.Controllers.Logins
{

    public class LoginController : BaseController
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IHostingEnvironment _hostingEnvironment;
        public Login _login;


        public LoginController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            _login = new Login(httpContextAccessor, config);


        }


        public IActionResult HospitalLogin()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            return View();
        }

        public IActionResult DiagnosticLabLogin()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            return View();
        }
        public IActionResult PharmacyLogin()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetHospitalLogin(HospitalsVendorsLoginModel item)
        {
            HospitalsVendorsLoginModel result = new HospitalsVendorsLoginModel();
            result = await _login.SetHospitalLogin(item);

            if (result.returnId != -1)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid,Convert.ToString(result.HospitalVendorID)),

                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, "Zyael");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                await HttpContext.SignInAsync(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                    principal, props);
                return RedirectToAction("HospitalDashBoard", "Hospitals", result);


            }
            else
            {
                TempData["ErrorMessage"] = "Invalid Credentials";
                return RedirectToAction("HospitalLogin", "Login");
                //return Json("unsuccessful");
                //return Json(result.returnId);



            }


        }
        [HttpPost]
        public async Task<IActionResult> SetDiagnosticLabLogin(DiagnosticLabVendorsLoginModel item)
        {
            DiagnosticLabVendorsLoginModel result = new DiagnosticLabVendorsLoginModel();
            result = await _login.SetDiagnosticLabLogin(item);

            if (result.returnId != -1)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,Convert.ToString(result.DLVID)),

                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, "Zyael");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                await HttpContext.SignInAsync(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                    principal, props);
                return RedirectToAction("DiagnosticLabDashBoard", "DiagnosticLab", result);


            }
            else
            {
                TempData["ErrorMessage"] = "Invalid Credentials";
                return RedirectToAction("DiagnosticLabLogin", "Login");
                //return Json("unsuccessful");
                //return Json(result.returnId);



            }

        }


        [HttpPost]
        public async Task<IActionResult> SetPharmacyLogin(PharmacyVendorsLoginModel item)
        {
            PharmacyVendorsLoginModel result = new PharmacyVendorsLoginModel();
            result = await _login.SetPharmacyLogin(item);

            if (result.returnId != -1)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid,Convert.ToString(result.PharmacyVendorID)),

                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, "Zyael");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                await HttpContext.SignInAsync(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                    principal, props);
                return RedirectToAction("PharmacyDashBoard", "Pharmacys", result);


            }
            else
            {
                TempData["ErrorMessage"] = "Invalid Credentials";
                return RedirectToAction("PharmacyLogin", "Login");
                //return Json("unsuccessful");
                //return Json(result.returnId);



            }

        }

    }
}

