using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Outreach.Entities.SuperModel
{
    public class Personnel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime? BirthDate { get; set; }
    }
}