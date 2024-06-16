using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWebServices.DAL;

namespace ZyaelWeb_DAL.DiagnosticLabs
{
          public class DiagnosticLabDAL : SqlDAL
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public DiagnosticLabDAL(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _config = config;

        }
    
    }
}
