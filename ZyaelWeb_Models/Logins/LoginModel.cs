using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZyaelWeb_Models.Logins
{
    public class HospitalsVendorsLoginModel
    {
        public int HospitalVendorID { get; set; }
        public string HospitalVendorEmail { get; set; }
        public string HospitalVendorPassword { get; set; }
        public string HospitalVendorUserName { get; set; }

        public int returnId { get; set; }

        public bool status { get; set; }

    }
}
