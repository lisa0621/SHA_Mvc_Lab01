using MotoGP.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGP.ViewModels.Team_Rider
{
    public class Team_RiderItemViewModel
    {
        public int Year { get; set; }

        public string TeamName { get; set; }

        public EnumGameClass Class { get; set; }

        public EnumBikeCompany Company { get; set; }

        public string RiderName { get; set; }

        public string Country { get; set; }

        public int Age { get; set; }
    }
}
