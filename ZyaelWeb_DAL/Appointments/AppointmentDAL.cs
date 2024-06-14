using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_Models.Appointments;
using ZyaelWeb_Models.Patients;
using ZyaelWebServices.DAL;

namespace ZyaelWeb_DAL.Appointments
{
    public class AppointmentDAL : SqlDAL
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public AppointmentDAL(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _config = config;

        }

        public async Task<List<OnlineAppointmentModel>> getOnlineAppointmentsGridDetails(int pageNumber, int pageSize, string sortBy, string sortingOrder, string searchinputText, int HospitalVendorID, int UserID)
        {
            try
            {
                string search = string.Empty;
                //if (!string.IsNullOrEmpty(searchinputText))
                //{
                //    search = "and (InstitutionName like'%" + searchinputText + "%')";
                //}
                using (SqlConnection con = GetConnection())
                {
                    con.Open();
                    var Param =
                        new
                        {
                            pageNumber = pageNumber,
                            pageSize = pageSize,
                            sortBy = sortBy,
                            sortingOrder = sortingOrder,
                            searchinputText = search,
                            HospitalVendorID = HospitalVendorID,
                            UserID = UserID
                        };
                    return (await con.QueryAsync<OnlineAppointmentModel>("SP_getOnlineAppointmentGridDetailsList", Param, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
