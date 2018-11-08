using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outreach.Entities.Models.HelthCenter
{
    public class HealthCenter
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string HealthCenterName { get; set; }

    }
}