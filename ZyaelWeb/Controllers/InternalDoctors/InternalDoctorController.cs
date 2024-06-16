using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Policy;
using System.Web.WebPages.Html;
using ZyaelWeb_Models.InternalDoctor;
using ZyaelWeb_Models.Logins;
using ZyaelWeb_Services.Admins;
using ZyaelWeb_Services.Hospital;
using ZyaelWeb_Services.InternalDoctor;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ZyaelWeb.Controllers.InternalDoctors
{
  
        public class InternalDoctorController : BaseController
        {
            readonly IHttpContextAccessor _httpContextAccessor;
            readonly IHostingEnvironment _hostingEnvironment;
            public InternalDoctor _internaldoctor;


            public InternalDoctorController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IConfiguration config)
            {
                this._hostingEnvironment = hostingEnvironment;
                this._httpContextAccessor = httpContextAccessor;
            _internaldoctor = new InternalDoctor(httpContextAccessor, config);


            }


        [HttpGet]
        public async Task<IActionResult> InternalDoctorDetailsAdd(int IDoctorID)
        {
            InternalDoctorModel item = new InternalDoctorModel();
            

            if (IDoctorID > 0)
            {
                item = await _internaldoctor.InternalDoctorDetailsAdd(IDoctorID);

                item.IDoctorID = IDoctorID;
            }

            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> InhouseDoctorProfile(int IDoctorID)
        {
            InternalDoctorModel item = new InternalDoctorModel();
            if (IDoctorID > 0)
            {
                item = await _internaldoctor.InternalDoctorDetailsAdd(IDoctorID);

                item.IDoctorID = IDoctorID;
            }

            return View(item);
        }



        [HttpPost]
        public async Task<IActionResult> InternalDoctorDetails_InsertUpdate(InternalDoctorModel item)
        {
            InternalDoctorModel test = new InternalDoctorModel();
            item.HospitalVendorID = HospitalVendorID;
            if (item.IDoctorProfileImage != null)
            {
                try
                {
                    var samplefilepath = $"{this._hostingEnvironment.ContentRootPath}" + "/" + "IDoctorProfileImageUpload" + "/" + "IDoctorProfileImage" + "/";
                    var fileName = ContentDispositionHeaderValue.Parse(item.IDoctorProfileImage.ContentDisposition).FileName;
                    var filesize = ContentDispositionHeaderValue.Parse(item.IDoctorProfileImage.ContentDisposition).Size;
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
                        "IDoctorProfileImageUpload" + "/",
                        FileGuid + extension);
                    item.IDoctorProfileImagePath = "/" + "IDoctorProfileImage" + "/" + FileGuid + extension;
                    item.IDoctorProfileImageName = fileName;
                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await item.IDoctorProfileImage.CopyToAsync(stream);
                    }
                    item.IDoctorProfileImagePath = "https://zyael-api.scm.azurewebsites.net/api/vfs/site/wwwroot/" + fullFilePath;
                }
                catch (Exception ex)
                {
                    item.IDoctorProfileImageName = "";
                }
            }
            var result = await _internaldoctor.InternalDoctorDetails_InsertUpdate(item);

            return RedirectToAction("InternalDoctorsGrid", "InternalDoctor");
        }


        public IActionResult InternalDoctorsGrid()
            {
            ShiftSlotModel item = new ShiftSlotModel();
            List<SelectListItem> obj = new List<SelectListItem>();
            obj.Add(new SelectListItem() { Text = "12:00AM", Value = "12:00AM" });
            obj.Add(new SelectListItem() { Text = "12:30AM", Value = "12:30AM" });
            obj.Add(new SelectListItem() { Text = "01:00AM", Value = "01:00AM" });
            obj.Add(new SelectListItem() { Text = "01:30AM", Value = "01:30AM" });
            obj.Add(new SelectListItem() { Text = "02:00AM", Value = "02:00AM" });
            obj.Add(new SelectListItem() { Text = "02:30AM", Value = "02:30AM" });
            obj.Add(new SelectListItem() { Text = "03:00AM", Value = "03:00AM" });
            obj.Add(new SelectListItem() { Text = "03:30AM", Value = "03:30AM" });
            obj.Add(new SelectListItem() { Text = "04:00AM", Value = "04:00AM" });
            obj.Add(new SelectListItem() { Text = "04:30AM", Value = "04:30AM" });
            obj.Add(new SelectListItem() { Text = "05:00AM", Value = "05:30AM" });
            obj.Add(new SelectListItem() { Text = "06:00AM", Value = "06:00AM" });
            obj.Add(new SelectListItem() { Text = "06:30AM", Value = "06:30AM" });
            obj.Add(new SelectListItem() { Text = "07:00AM", Value = "07:00AM" });
            obj.Add(new SelectListItem() { Text = "07:30AM", Value = "07:30AM" });
            obj.Add(new SelectListItem() { Text = "08:00AM", Value = "08:00AM" });
            obj.Add(new SelectListItem() { Text = "08:30AM", Value = "08:30AM" });
            obj.Add(new SelectListItem() { Text = "09:00AM", Value = "09:00AM" });
            obj.Add(new SelectListItem() { Text = "09:30AM", Value = "09:30AM" });
            obj.Add(new SelectListItem() { Text = "10:00AM", Value = "10:00AM" });
            obj.Add(new SelectListItem() { Text = "10:30AM", Value = "10:30AM" });
            obj.Add(new SelectListItem() { Text = "11:00AM", Value = "11:00AM" });
            obj.Add(new SelectListItem() { Text = "11:30AM", Value = "11:30AM" });
            obj.Add(new SelectListItem() { Text = "12:00PM", Value = "12:00PM" });
            obj.Add(new SelectListItem() { Text = "12:30PM", Value = "12:30PM" });
            obj.Add(new SelectListItem() { Text = "01:00PM", Value = "01:00PM" });
            obj.Add(new SelectListItem() { Text = "01:30PM", Value = "01:30PM" });
            obj.Add(new SelectListItem() { Text = "02:00PM", Value = "02:00PM" });
            obj.Add(new SelectListItem() { Text = "02:30PM", Value = "02:30PM" });
            obj.Add(new SelectListItem() { Text = "03:00PM", Value = "03:00PM" });
            obj.Add(new SelectListItem() { Text = "03:30PM", Value = "03:30PM" });
            obj.Add(new SelectListItem() { Text = "04:00PM", Value = "04:00PM" });
            obj.Add(new SelectListItem() { Text = "04:30PM", Value = "04:30PM" });
            obj.Add(new SelectListItem() { Text = "05:00PM", Value = "05:30PM" });
            obj.Add(new SelectListItem() { Text = "06:00PM", Value = "06:00PM" });
            obj.Add(new SelectListItem() { Text = "06:30PM", Value = "06:30PM" });
            obj.Add(new SelectListItem() { Text = "07:00PM", Value = "07:00PM" });
            obj.Add(new SelectListItem() { Text = "07:30PM", Value = "07:30PM" });
            obj.Add(new SelectListItem() { Text = "08:00PM", Value = "08:00PM" });
            obj.Add(new SelectListItem() { Text = "08:30PM", Value = "08:30PM" });
            obj.Add(new SelectListItem() { Text = "09:00PM", Value = "09:00PM" });
            obj.Add(new SelectListItem() { Text = "09:30PM", Value = "09:30PM" });
            obj.Add(new SelectListItem() { Text = "10:00PM", Value = "10:00PM" });
            obj.Add(new SelectListItem() { Text = "10:30PM", Value = "10:30PM" });
            obj.Add(new SelectListItem() { Text = "11:00PM", Value = "11:00PM" });
            obj.Add(new SelectListItem() { Text = "11:30PM", Value = "11:30PM" });
            item.drpSubjects = obj;
            return View(item);
        }
      

        [HttpPost]
        public async Task<IActionResult> getInternalDoctorsDetails(int pageNumber, int pageSize,int IDoctorID)
        {
            var recordsTotal = 0;
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            string searchinputText = HttpContext.Request.Form["search[value]"].FirstOrDefault();
            var sortingOrder = HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();
            var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"].FirstOrDefault();
            var start = HttpContext.Request.Form["[start]"].FirstOrDefault();
            var length = HttpContext.Request.Form["[length]"].FirstOrDefault();
            List<InternalDoctorModel> list = new List<InternalDoctorModel>();

            list = await _internaldoctor.getInternalDoctorsDetails(pageNumber, pageSize, sortBy, sortingOrder, searchinputText, HospitalVendorID, IDoctorID);
            if (list != null && list.Count > 0)
                if (list != null && list.Count > 0)
                {
                    recordsTotal = list[0].TotalrowCount;
                }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
        }


        [HttpPost]
        public async Task<IActionResult> SetInternalDoctorActiveStatus(InternalDoctorModel item)
        {
            var result = await _internaldoctor.SetInternalDoctorActiveStatus(item);
            return Json(result);
        }


        [HttpPost]
        public async Task<IActionResult> SetInternalDoctorSlots(ShiftSlotModel item)
        {
            item.HospitalVendorID=HospitalVendorID; 
            var result = await _internaldoctor.SetInternalDoctorSlots(item);

            return RedirectToAction("InternalDoctorsGrid", "InternalDoctor");
        }

        [HttpPost]
        public async Task<IActionResult> GetInternalDoctorSlotsByDateandID(int IDoctorID, DateTime Date)
        {
            List<ShiftSlotModel> result = new List<ShiftSlotModel>();
            //List<slots> res = new List<slots>();
            result = await _internaldoctor.GetInternalDoctorSlotsByDateandID(IDoctorID, Date);
            return Ok(result);
        }


    }
}

