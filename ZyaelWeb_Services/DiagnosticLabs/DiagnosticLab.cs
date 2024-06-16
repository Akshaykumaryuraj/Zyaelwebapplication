using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyaelWeb_DAL.Appointments;
using ZyaelWeb_DAL.DiagnosticLabs;

namespace ZyaelWeb_Services.DiagnosticLabs
{
         public class DiagnosticLab
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        public DiagnosticLabDAL _diagnosticlabLdal;
        public DiagnosticLab(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            this._httpContextAccessor = httpContextAccessor;
            _diagnosticlabLdal = new DiagnosticLabDAL(httpContextAccessor, config);
        }


    
    }
}
