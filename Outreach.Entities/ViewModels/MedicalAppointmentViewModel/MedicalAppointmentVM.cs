using Outreach.Entities.Models.Doctor;
using Outreach.Entities.Models.HelthCenter;
using Outreach.Entities.Models.ServiceUser.MedicalAppointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outreach.Entities.ViewModels.MedicalAppointmentViewModel
{
    public class MedicalAppointmentVM
    {
        public int HealthCenterId { get; set; }
        public MedicalAppointment MedicalAppointments { get; set; }
        public HealthCenter HealthCenter { get; set; }
        public Doctor doctor { get; set; }
        public int DoctorId { get; set; }

    }
}