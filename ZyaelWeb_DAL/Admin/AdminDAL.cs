using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_Models.Logins;
using ZyaelWebServices.DAL;

namespace ZyaelWeb_DAL.Admin
{
    public class AdminDAL : SqlDAL
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public AdminDAL(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _config = config;

        }
        public async Task<List<VendorsCredentialModel>> getVendorsCredentialDetails(int pageNumber, int pageSize, string sortBy, string sortingOrder, string searchinputText, string vendor)
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
                            vendor= vendor
                        };
                    return (await con.QueryAsync<VendorsCredentialModel>("SP_getVendorsCredentialList", Param, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
