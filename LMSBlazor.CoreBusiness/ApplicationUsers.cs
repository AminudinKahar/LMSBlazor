using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSBlazor.CoreBusiness
{
    public class ApplicationUsers : IdentityUser
    {
        public string FullName { get; set; } = String.Empty;
        [DisplayName("Tandatangan")]
        public string Tandatangan { get; set; } = String.Empty;

        //relationship
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
