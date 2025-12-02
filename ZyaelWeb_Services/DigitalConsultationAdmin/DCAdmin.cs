using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_DAL.Admin;
using ZyaelWeb_DAL.DigitalConsultationAdmin;
using ZyaelWeb_Models.DigitalConsultationAdmin;
using ZyaelWeb_Models.Logins;

namespace ZyaelWeb_Services.DigitalConsultationAdmin
{
    public class DCAdmin
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        public DCAdminDAL _dcadmindal;
        public DCAdmin(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _dcadmindal = new DCAdminDAL(httpContextAccessor, config);
        }

        public async Task<List<DoctorProfileModel>> getDCDoctorProfileDetails(int pageNumber, int pageSize, string sortBy, string sortingOrder, string searchinputText, int DoctorID)
        {
            try
            {
                var result = await _dcadmindal.getDCDoctorProfileDetails(pageNumber, pageSize, sortBy, sortingOrder, searchinputText, DoctorID);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DoctorProfileModel> DCDProfileDetailsAdd(int DoctorPId)
        {
            try
            {
                var result = await _dcadmindal.DCDProfileDetailsAdd(DoctorPId);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> DCDoctorProfileDetails_InsertUpdate(DoctorProfileModel item)
        {
            try
            {
                var result = await _dcadmindal.DCDoctorProfileDetails_InsertUpdate(item);
                return result;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> SetDoctorProfileStatus(DoctorProfileModel item)
        {
            try
            {
                var result = await _dcadmindal.SetDoctorProfileStatus(item);
                return result;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        public async Task<List<DoctorActivationModel>> getDCDoctorActiveDetails(int pageNumber, int pageSize, string sortBy, string sortingOrder, string searchinputText, int DoctorID)
        {
            try
            {
                var result = await _dcadmindal.getDCDoctorActiveDetails(pageNumber, pageSize, sortBy, sortingOrder, searchinputText, DoctorID);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<int> SetDoctorActiveStatus(DoctorActivationModel item)
        {
            try
            {
                var result = await _dcadmindal.SetDoctorActiveStatus(item);
                return result;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> SetDoctorGoLiveStatus(DoctorActivationModel item)
        {
            try
            {
                var result = await _dcadmindal.SetDoctorGoLiveStatus(item);
                return result;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        public async Task<List<ConsultationSlotDateModel>> GetDoctorSlotsByDateandID(int DoctorID,DateTime Date)
        {
            try
            {
                var result = await _dcadmindal.GetDoctorSlotsByDateandID(DoctorID, Date);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<ConsultationSlotDateModel>> GetAvailableSlots(int DoctorID)
        {
            try
            {
                var result = await _dcadmindal.GetAvailableSlots(DoctorID);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<UserAppointmentsModel>> GetUserAppointmentByDoctorID(int DoctorID, string status, string AppointmentDate,string SearchText)
        {
            try
            {
                var result = await _dcadmindal.GetUserAppointmentByDoctorID(DoctorID, status, AppointmentDate, SearchText);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<UserAppointmentsModel>> getDCDoctorsDetails(int pageNumber, int pageSize, string sortBy, string sortingOrder, string searchinputText)
        {
            try
            {
                var result = await _dcadmindal.getDCDoctorsDetails(pageNumber, pageSize, sortBy, sortingOrder, searchinputText);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
