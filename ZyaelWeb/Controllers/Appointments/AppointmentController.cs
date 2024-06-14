using Microsoft.AspNetCore.Mvc;
using ZyaelWeb_Models.Appointments;
using ZyaelWeb_Models.Patients;
using ZyaelWeb_Services.Appointments;
using ZyaelWeb_Services.InternalDoctor;
using ZyaelWeb_Services.Patients;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ZyaelWeb.Controllers.Appointments
{
    public class AppointmentController : BaseController
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IHostingEnvironment _hostingEnvironment;
        public Appointment _appointment;


        public AppointmentController(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            _appointment = new Appointment(httpContextAccessor, config);


        }

        public IActionResult AppointmentsGrid()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> getOnlineAppointmentsGridDetails(int pageNumber, int pageSize, int UserID)
        {
            var recordsTotal = 0;
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            string searchinputText = HttpContext.Request.Form["search[value]"].FirstOrDefault();
            var sortingOrder = HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();
            var sortBy = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"].FirstOrDefault();
            var start = HttpContext.Request.Form["[start]"].FirstOrDefault();
            var length = HttpContext.Request.Form["[length]"].FirstOrDefault();
            List<OnlineAppointmentModel> list = new List<OnlineAppointmentModel>();

            list = await _appointment.getOnlineAppointmentsGridDetails(pageNumber, pageSize, sortBy, sortingOrder, searchinputText, HospitalVendorID, UserID);
            if (list != null && list.Count > 0)
                if (list != null && list.Count > 0)
                {
                    recordsTotal = list[0].TotalrowCount;
                }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
        }

    }
}
