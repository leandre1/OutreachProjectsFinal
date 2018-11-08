using Outreach.Entities.DTO.ServiceUserDto;
using Outreach.Entities.Models.ServiceUser;
using Outreach.Entities.StaffEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outreach.Entities.ViewModels.ServiceUserFormViewModel
{
    public class ServiceUserFormVM
    {
        public ServiceUser ServiceUser { get; set; }
        public Staff staff { get; set; }
       

    }
}