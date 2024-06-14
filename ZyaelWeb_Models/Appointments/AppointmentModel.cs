using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZyaelWeb_Models.Appointments
{
    public class AppointmentModel
    {
    }
    public class OnlineAppointmentModel
    {
        public int UserID { get; set; }
        public int IDoctorID { get; set; }

        public string UserName { get; set; }
        public int HospitalProfileID { get; set; }
        public int HospitalVendorID { get; set; }
        public DateTime Date { get; set; }
        public string UserSelectedSlot { get; set; }
        public int UserRandomID { get; set; }
        public int ConsultationFees { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Gender { get; set; }
        public String Experience { get; set; }
        public Int64 Phone { get; set; }
        public String ConsultationCategory { get; set; }
        public String City { get; set; }
        public String ProficientLanguage { get; set; }
        public String Specialization { get; set; }
        public string PaymentID { get; set; }
        public string PaymentOrderID { get; set; }
        public string Status { get; set; }
        public int TotalrowCount { get; set; }

    }
}
