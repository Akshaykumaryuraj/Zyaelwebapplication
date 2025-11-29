using Microsoft.AspNetCore.Mvc;
using ZyaelWeb_Models.DigitalConsultationAdmin;
using ZyaelWeb_Models.Logins;
using ZyaelWeb_Services.Admins;
using ZyaelWeb_Services.DigitalConsultationAdmin;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ZyaelWeb.Controllers.DigitalConsultationAdmin
{
    public class DCAdminController : BaseController
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IHostingEnvironment _hostingEnvironment;
        public DCAdmin _dcadmin;


        public DCAdminController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            _dcadmin = new DCAdmin(httpContextAccessor, config);

        }

        public IActionResult DigitalConsultationAdminDashBoard()
        {
            return View();
        }

        public IActionResult DCDoctorProfileGrid()
        {
            return View();
        }

        public IActionResult DCDoctorActivationGrid()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> getDCDoctorProfileDetails(int pageNumber, int pageSize,int DoctorID)
        {
            var recordsTotal = 0;
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            string searchinputText = HttpContext.Request.Form["search[value]"].FirstOrDefault();
            var sortingOrder = HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();
            var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"].FirstOrDefault();
            var start = HttpContext.Request.Form["[start]"].FirstOrDefault();
            var length = HttpContext.Request.Form["[length]"].FirstOrDefault();
            List<ZyaelWeb_Models.DigitalConsultationAdmin.DoctorProfileModel> list = new List<DoctorProfileModel>();

            list = await _dcadmin.getDCDoctorProfileDetails(pageNumber, pageSize, sortBy, sortingOrder, searchinputText, DoctorID);
            if (list != null && list.Count > 0)
                if (list != null && list.Count > 0)
                {
                    recordsTotal = list[0].TotalrowCount;
                }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
        }


        [HttpGet]
        public async Task<IActionResult> DCDProfileDetailsAdd(int DoctorPId)
        {
            DoctorProfileModel item = new DoctorProfileModel();

            if (DoctorPId > 0)
            {
                item = await _dcadmin.DCDProfileDetailsAdd(DoctorPId);

                item.DoctorPId = DoctorPId;
            }

            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctorProfileDetails(int DoctorPId)
        {
            DoctorProfileModel item = new DoctorProfileModel();

            if (DoctorPId > 0)
            {
                item = await _dcadmin.DCDProfileDetailsAdd(DoctorPId);

                item.DoctorPId = DoctorPId;
            }

            return Json(item);
        }


        [HttpPost]
        public async Task<IActionResult> DCDoctorProfileDetails_InsertUpdate(DoctorProfileModel item)
        {
            DoctorProfileModel test = new DoctorProfileModel();

            var result = await _dcadmin.DCDoctorProfileDetails_InsertUpdate(item);

            return RedirectToAction("DCDoctorProfileGrid", "DCAdmin");

        }

        [HttpPost]
        public async Task<IActionResult> SetDoctorProfileStatus(DoctorProfileModel item)
        {
            var result = await _dcadmin.SetDoctorProfileStatus(item);
            return Json(result);
        }



        [HttpPost]
        public async Task<IActionResult> getDCDoctorActiveDetails(int pageNumber, int pageSize, int DoctorID)
        {
            var recordsTotal = 0;
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            string searchinputText = HttpContext.Request.Form["search[value]"].FirstOrDefault();
            var sortingOrder = HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();
            var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"].FirstOrDefault();
            var start = HttpContext.Request.Form["[start]"].FirstOrDefault();
            var length = HttpContext.Request.Form["[length]"].FirstOrDefault();
            List<ZyaelWeb_Models.DigitalConsultationAdmin.DoctorActivationModel> list = new List<DoctorActivationModel>();

            list = await _dcadmin.getDCDoctorActiveDetails(pageNumber, pageSize, sortBy, sortingOrder, searchinputText, DoctorID);
            if (list != null && list.Count > 0)
                if (list != null && list.Count > 0)
                {
                    recordsTotal = list[0].TotalrowCount;
                }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
        }


        [HttpPost]
        public async Task<IActionResult> SetDoctorActiveStatus(DoctorActivationModel item)
        {
            var result = await _dcadmin.SetDoctorActiveStatus(item);
            return Json(result);
        }


        [HttpPost]
        public async Task<IActionResult> SetDoctorGoLiveStatus(DoctorActivationModel item)
        {
            var result = await _dcadmin.SetDoctorGoLiveStatus(item);
            return Json(result);
        }



        [HttpGet]
        public async Task<IActionResult> GetDoctorSlotsByDateandID(int DoctorPId, DateTime Date)
        {
            List<ConsultationSlotDateModel> result = new List<ConsultationSlotDateModel>();
            result = await _dcadmin.GetDoctorSlotsByDateandID(DoctorPId, Date);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableSlots(int DoctorPId)
        {
            List<ConsultationSlotDateModel> item = new List<ConsultationSlotDateModel>();
            if (DoctorPId > 0)
            {
                item = await _dcadmin.GetAvailableSlots(DoctorPId);
            }

            return Json(item);
        }


    }
}

