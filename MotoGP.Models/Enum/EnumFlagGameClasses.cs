using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGP.Models.Enum
{
    [Flags]
    public enum EnumFlagGameClasses
    {
        MotoGP = 1,
        Moto2 = 2,
        Moto3 = 4,
        [Description("250cc")]
        [Display(Name = "250cc")]
        cc250 = 8,
        [Description("125cc")]
        [Display(Name = "125cc")]
        cc125 = 16
    }
}
