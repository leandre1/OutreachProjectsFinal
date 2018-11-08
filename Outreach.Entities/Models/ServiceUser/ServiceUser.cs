
using Outreach.Entities.House;
using Outreach.Entities.StaffEmployee;
using Outreach.Entities.SuperModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Outreach.Entities.Models.ServiceUser
{
    public class ServiceUser:Personnel
    { 
  
        public string ImageUrl { get; set; }
        public int DoctorId { get; set; }

        [Display(Name ="Keyworker")]
        public int StaffId { get; set; }
    
        public Staff staff { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}