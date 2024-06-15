using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZyaelWeb_Models.Admins
{
    
        public class SpecialitiesModel
        {
            public int SpecialityID { get; set; }
            public int TotalrowCount { get; set; }
            public string SpecialityName { get; set; }
            public string SpecialityCode { get; set; }
            public string SpecialityImage { get; set; }
            public int returnId { get; set; }
            public string message { get; set; }
            public string Symptoms { get; set; }
            public bool status { get; set; }
        }
    
}
