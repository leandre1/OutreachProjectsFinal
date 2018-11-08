using Outreach.Entities.House;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outreach.Entities.ViewModels.BranchFormViewModel
{
    public class BranchFormViewModel
    {
        public Branch Branch { get; set; }
        public List<RootHouse> Houses { get; set; }
    }
}