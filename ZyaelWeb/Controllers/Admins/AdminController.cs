using Microsoft.AspNetCore.Mvc;
using ZyaelWeb_Models.Logins;
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

        [HttpPost]
        public async Task<IActionResult> getVendorsCredentialDetails(int pageNumber, int pageSize, string vendor)
        {
            var recordsTotal = 0;
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            string searchinputText = HttpContext.Request.Form["search[value]"].FirstOrDefault();
            var sortingOrder = HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();
            var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"].FirstOrDefault();
            var start = HttpContext.Request.Form["[start]"].FirstOrDefault();
            var length = HttpContext.Request.Form["[length]"].FirstOrDefault();
            List<VendorsCredentialModel> list = new List<VendorsCredentialModel>();

            list = await _admin.getVendorsCredentialDetails(pageNumber, pageSize, sortBy, sortingOrder, searchinputText, vendor);
            if (list != null && list.Count > 0)
                if (list != null && list.Count > 0)
                {
                    recordsTotal = list[0].TotalrowCount;
                }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
        }

    }
}
