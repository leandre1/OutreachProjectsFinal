using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outreach.Entities.Models.Doctor
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Specialist { get; set; }
        public int HealthCenterId { get; set; }
    }
}