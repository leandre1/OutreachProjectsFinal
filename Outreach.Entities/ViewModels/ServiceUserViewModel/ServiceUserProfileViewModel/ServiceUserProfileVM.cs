using Outreach.Entities.Models.ServiceUser;
using Outreach.Entities.Models.ServiceUser.Menu;
using Outreach.Entities.ViewModels.ServiceUserViewModel.ServiceUserBranch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outreach.Entities.ViewModels.ServiceUserViewModel.ServiceUserProfileViewModel
{
    public class ServiceUserProfileVM : ServiceUserBranchVM
    {
       public List<Menu> Menus { get; set; }
       public List<ServiceUserMenu> UserMenu { get; set; }
       
    }
}