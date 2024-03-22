using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_Models.Logins;
using ZyaelWebServices.DAL;

namespace ZyaelWeb_DAL.Logins
{
    public class AdminLoginDAL:SqlDAL
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public AdminLoginDAL(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _config = config;

        }



        public async Task<AdminLoginModel> SetAdminLogin(AdminLoginModel item)
        {
            //var password = common.PasswordEncription(item.Password);
            try
            {
                var Connection = new SqlConnection(_config.GetConnectionString("DefautConnection"));
                using (SqlConnection con= GetConnection())
                {
                    con.Open();
                    var Param =
                        new
                        {

                            AdminEmail = item.Email,
                            AdminPassword = item.Password

                        };
                    return (await con.QueryAsync<AdminLoginModel>("sp_checkAdminLogin", Param, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
