using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZyaelWeb_Models.Logins
{
    public class HospitalsVendorsLoginModel
    {
        public int HospitalVendorID { get; set; }
        public string HospitalVendorEmail { get; set; }
        [Required(ErrorMessage = "Please Valid Email!")]
        public string HospitalVendorPassword { get; set; }
        [Required(ErrorMessage = "Please enter Password!")]
        public string HospitalVendorUserName { get; set; }

        public int returnId { get; set; }

        public bool status { get; set; }

    }
    public class DiagnosticLabVendorsLoginModel
    {
        //public int DiagnosticLabVendorID { get; set; }
        public int DLVID { get; set; }
        //public int DLVPID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //public string DiagnosticLabVendorEmail { get; set; }
        //[Required(ErrorMessage = "Please Valid Email!")]
        //public string DiagnosticLabVendorPassword { get; set; }
        //[Required(ErrorMessage = "Please enter Password!")]
        public string DiagnosticLabVendorUserName { get; set; }

        public int returnId { get; set; }

        public bool status { get; set; }

    }


    public class PharmacyVendorsLoginModel
    {
        public int PharmacyVendorID { get; set; }
        public string PharmacyVendorEmail { get; set; }
        [Required(ErrorMessage = "Please Valid Email!")]
        public string PharmacyVendorPassword { get; set; }
        [Required(ErrorMessage = "Please enter Password!")]
        public string PharmacyVendorUserName { get; set; }

        public int returnId { get; set; }

        public bool status { get; set; }

        public int PVID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string message { get; set; }
        public string Gender { get; set; }
        public Int64 Mobile { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

    }
}
