using Outreach.Entities.House;
using Outreach.Entities.Models.ServiceUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outreach.Entities.ViewModels.ServiceUserViewModel.ServiceUserBranch
{
    public class ServiceUserBranchVM
    {
        public ServiceUser User { get; set; }
        public Branch Branch { get; set; }
       
    }
}