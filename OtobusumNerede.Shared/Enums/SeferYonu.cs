using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtobusumNerede.Shared.Enums
{
    public enum SeferYonu
    {
        [Display(Name = "Gidiş")]
        Gidis,

        [Display(Name = "Dönüş")]
        Donus,

    }
}
