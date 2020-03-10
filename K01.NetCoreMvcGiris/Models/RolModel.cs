using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace K01.NetCoreMvcGiris.Models
{
    public class RolModel
    {
        [Display(Name="İsim")]
        [Required(ErrorMessage ="İsim alanı gereklidir.")]
        public string Name { get; set; }
    }
}
