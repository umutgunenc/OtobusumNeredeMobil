using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtobusumNerede.Shared.Enums
{
    public enum GunlerEnum
    {
        [Display(Name = "Hafta İçi")]
        HaftaIci,

        [Display(Name = "Cumartesi")]
        Cumartesi,

        [Display(Name = "Pazar")]
        Pazar
    }
}
