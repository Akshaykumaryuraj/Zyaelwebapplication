using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Policy;
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
            var result = await _internaldoctor.InternalDoctorDetails_InsertUpdate(item);

            return RedirectToAction("InternalDoctorsGrid", "InternalDoctor");
        }


        public IActionResult InternalDoctorsGrid()
            {

            return View();
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
        public async Task<IActionResult> SetInternalDoctorSlots(int IDoctorID, int HospitalVendorID, DateTime Date,List<Shifts> item)
        {

            var result = await _internaldoctor.SetInternalDoctorSlots(IDoctorID, HospitalVendorID, Date, item);

            return Json(result);
        }

    }
}

