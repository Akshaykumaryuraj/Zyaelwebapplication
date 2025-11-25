using Microsoft.AspNetCore.Mvc;
using ZyaelWeb_Models.InternalDoctor;
using ZyaelWeb_Models.Logins;
using ZyaelWeb_Models.Admins;
using ZyaelWeb_Services.Admins;
using ZyaelWeb_Services.InternalDoctor;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Net.Http.Headers;

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

        //TESTING GITHUB
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





        [HttpGet]
        public async Task<IActionResult> VendorProfileDetailsAdd(int DoctorID)
        {
            DoctorProfileModel item = new DoctorProfileModel();

            if (DoctorID > 0)
            {
                item = await _admin.VendorProfileDetailsAdd(DoctorID);

                item.DoctorID = DoctorID;
            }

            return View(item);
        }



        [HttpPost]
        public async Task<IActionResult> VendorProfileDetails_InsertUpdate(DoctorProfileModel item)
        {
            DoctorProfileModel test = new DoctorProfileModel();

            var result = await _admin.VendorProfileDetails_InsertUpdate(item);

            return RedirectToAction("VendorsCredentialGrid", "Admin");

        }


        [HttpPost]
        public async Task<IActionResult> SetVendorsLoginStatus(AdminLoginModel item)
        {
            var result = await _admin.SetVendorsLoginStatus(item);
            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> SpecialitiesDetailsAdd(int SpecialityID)
        {
            SpecialitiesModel item = new SpecialitiesModel();


            if (SpecialityID > 0)
            {
                item = await _admin.SpecialitiesDetailsAdd(SpecialityID);

                item.SpecialityID = SpecialityID;
            }

            return View(item);
        }


        [HttpPost]
        public async Task<IActionResult> SpecialitiesDetails_InsertUpdate(SpecialitiesModel item)
        {
            SpecialitiesModel test = new SpecialitiesModel();
            if (item.SpecialityProfileImage != null)
            {
                try
                {
                    var samplefilepath = $"{this._hostingEnvironment.ContentRootPath}" + "/" + "SpecialityProfileImageUpload" + "/" + "SpecialityProfileImage" + "/";
                    var fileName = ContentDispositionHeaderValue.Parse(item.SpecialityProfileImage.ContentDisposition).FileName;
                    var filesize = ContentDispositionHeaderValue.Parse(item.SpecialityProfileImage.ContentDisposition).Size;
                    fileName = fileName.Contains("\\")
                     ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                    : fileName.Trim('"');
                    if (!Directory.Exists(samplefilepath))
                    {
                        Directory.CreateDirectory(samplefilepath);
                    }
                    var extension = Path.GetExtension(fileName);
                    var FileGuid = Guid.NewGuid();
                    var fullFilePath = Path.Combine(
                        "SpecialityProfileImageUpload" + "/",
                        FileGuid + extension);
                    item.SpecialityProfileImagePath = "/" + "SpecialityProfileImage" + "/" + FileGuid + extension;
                    item.SpecialityProfileImageName = fileName;
                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await item.SpecialityProfileImage.CopyToAsync(stream);
                    }
                    item.SpecialityProfileImagePath = "https://zyael-api.scm.azurewebsites.net/api/vfs/site/wwwroot/" + fullFilePath;
                }
                catch (Exception ex)
                {
                    item.SpecialityProfileImageName = "";
                }
            }



            var result = await _admin.SpecialitiesDetails_InsertUpdate(item);

            return RedirectToAction("SpecializationsGrid", "Admin");

        }


        public IActionResult SpecializationsGrid()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> getSpecializationsDetails(int pageNumber, int pageSize, int SpecialityID)
        {
            var recordsTotal = 0;
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            string searchinputText = HttpContext.Request.Form["search[value]"].FirstOrDefault();
            var sortingOrder = HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();
            var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"].FirstOrDefault();
            var start = HttpContext.Request.Form["[start]"].FirstOrDefault();
            var length = HttpContext.Request.Form["[length]"].FirstOrDefault();
            List<SpecialitiesModel> list = new List<SpecialitiesModel>();

            list = await _admin.getSpecializationsDetails(pageNumber, pageSize, sortBy, sortingOrder, searchinputText, SpecialityID);
            if (list != null && list.Count > 0)
                if (list != null && list.Count > 0)
                {
                    recordsTotal = list[0].TotalrowCount;
                }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
        }


        [HttpPost]
        public async Task<IActionResult> SetSpecilizationStatus(SpecialitiesModel item)
        {
            var result = await _admin.SetSpecilizationStatus(item);
            return Json(result);
        }


        [HttpPost]
        public async Task<IActionResult> SetSpecilizationPriority(SpecialitiesModel item)
        {
            var result = await _admin.SetSpecilizationPriority(item);
            return Json(result);
        }
    }
}
