using Outreach.Entities.House;
using Outreach.Entities.StaffEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outreach.Entities.StaffFormViewModel
{
    public class StaffFormViewModel
    {
        public Staff Staff { get; set; }
        public List<CategoryStaff> Categories { get; set; }
        public List<Branch> Branches { get; set; }

    }
}