
using Outreach.Entities.SuperModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Outreach.Entities.StaffEmployee
{
    public class Staff : Personnel
    {
     
        [Display(Name = "Date of Registration")]
        [Required]
        public DateTime? DateOfRegistration { get; set; }

        [Display(Name ="Category")]
        public int CategoryId { get; set; }
    }
}