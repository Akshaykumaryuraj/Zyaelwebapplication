using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_DAL.Appointments;
using ZyaelWeb_DAL.InternalDoctor;
using ZyaelWeb_DAL.Patients;
using ZyaelWeb_Models.Appointments;
using ZyaelWeb_Models.Patients;

namespace ZyaelWeb_Services.Appointments
{
   public class Appointment
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        public AppointmentDAL _appointmentdal;
        public Appointment(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _appointmentdal = new AppointmentDAL(httpContextAccessor, config);
        }



        public async Task<List<OnlineAppointmentModel>> getOnlineAppointmentsGridDetails(int pageNumber, int pageSize, string sortBy, string sortingOrder, string searchinputText, int HospitalVendorID, int UserID)
        {
            try
            {
                var result = await _appointmentdal.getOnlineAppointmentsGridDetails(pageNumber, pageSize, sortBy, sortingOrder, searchinputText, HospitalVendorID, UserID);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
