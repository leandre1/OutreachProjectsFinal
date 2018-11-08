using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Outreach.Entities.House
{
    public class Branch
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Display(Name="House")]
        public int HouseId { get; set; }
        public string PhoneNumber { get; set; }
        //public RootHouse House { get; set; }

    }
}