using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outreach.Data.Interface
{
    public interface IPersonnel
    {
         int Id { get; set; }
        [Required]
         string FirstName { get; set; }
        [Required]
        string LastName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        DateTime? BirthDate { get; set; }
    }
}
