using Outreach.Entities.Models.Doctor;
using Outreach.Entities.Models.HelthCenter;
using Outreach.Entities.Models.ServiceUser;
using Outreach.Entities.Models.ServiceUser.MedicalAppointment;
using Outreach.Entities.Models.ServiceUser.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outreach.Entities.ViewModels.ServiceUserViewModel
{
    public class ServiceUserMenuVM
    {
        public ServiceUser User { get; set; }
        public ServiceUserMenu UserMenu { get; set; }

        //public int HealthCenterId { get; set; }
        //public List<MedicalAppointment> MedicalAppointments { get; set; }
        //public HealthCenter HealthCenter { get; set; }
        //public Doctor Doctor { get; set; }
    }
}