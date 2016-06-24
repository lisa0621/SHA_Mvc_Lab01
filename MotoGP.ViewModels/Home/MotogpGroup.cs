using MotoGP.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGP.ViewModels.Home
{
    public class MotogpGroup
    {
        public EnumFlagGameClasses Classes { get; set; }

        public int? RiderCount { get; set; }
    }
}
