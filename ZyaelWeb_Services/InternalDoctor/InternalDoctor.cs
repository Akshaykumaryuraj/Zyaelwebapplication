using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_DAL.Admin;
using ZyaelWeb_DAL.Hospitals;
using ZyaelWeb_DAL.InternalDoctor;
using ZyaelWeb_Models.InternalDoctor;
using ZyaelWeb_Models.Logins;

namespace ZyaelWeb_Services.InternalDoctor
{
    public class InternalDoctor
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        public InternalDoctorDAL _internaldoctordal;
        public InternalDoctor(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _internaldoctordal = new InternalDoctorDAL(httpContextAccessor, config);
        }


        public async Task<InternalDoctorModel> InternalDoctorDetailsAdd(int IDoctorID)
        {
            try
            {
                var result = await _internaldoctordal.InternalDoctorDetailsAdd(IDoctorID);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<int> InternalDoctorDetails_InsertUpdate(InternalDoctorModel item)
        {
            try
            {
                var result = await _internaldoctordal.InternalDoctorDetails_InsertUpdate(item);
                return result;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        public async Task<List<InternalDoctorModel>> getInternalDoctorsDetails(int pageNumber, int pageSize, string sortBy, string sortingOrder, string searchinputText, int HospitalVendorID,int IDoctorID)
        {
            try
            {
                var result = await _internaldoctordal.getInternalDoctorsDetails(pageNumber, pageSize, sortBy, sortingOrder, searchinputText, HospitalVendorID, IDoctorID);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<int> SetInternalDoctorActiveStatus(InternalDoctorModel item)
        {
            try
            {
                var result = await _internaldoctordal.SetInternalDoctorActiveStatus(item);
                return result;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> SetInternalDoctorSlots(ShiftSlotModel item)
        {
            try
            {
                var result = await _internaldoctordal.SetInternalDoctorSlots(item);
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<ShiftSlotModel> GetIDoctorDetails(int IDoctorID)
        {
            try
            {
                var result = await _internaldoctordal.GetIDoctorDetails(IDoctorID);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}

