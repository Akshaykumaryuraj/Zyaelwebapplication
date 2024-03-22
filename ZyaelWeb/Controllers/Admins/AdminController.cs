using Microsoft.AspNetCore.Mvc;
using ZyaelWeb_Services.Admins;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ZyaelWeb.Controllers.Admins
{
    public class AdminController : BaseController
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IHostingEnvironment _hostingEnvironment;
        public Admin _admin;


        public AdminController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            _admin = new Admin(httpContextAccessor, config);

        }


        public IActionResult AdminDashBoard()
        {
            return View();
        }

        public IActionResult AdminProfile()
        {
            return View();
        }

        public IActionResult VendorsCredentialGrid()
        {
            return View();
        }
    }
}
