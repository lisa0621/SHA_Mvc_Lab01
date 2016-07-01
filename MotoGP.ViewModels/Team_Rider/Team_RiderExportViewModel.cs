using MotoGP.Models.Enum;
using SHA_Mvc_Lab01.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGP.ViewModels.Team_Rider
{
    public class Team_RiderExportViewModel
    {
        [ExportColumn(Name = "Year", Order = 1)]
        public int Year { get; set; }

        [ExportColumn(Name = "Team Name", Order = 3)]
        public string TeamName { get; set; }

        [ExportColumn(Name = "Class", Order = 2)]
        public EnumGameClass Class { get; set; }

        [ExportColumn(Name = "Company", Order = 4)]
        public EnumBikeCompany Company { get; set; }

        [ExportColumn(Name = "Rider Name", Order = 5)]
        public string RiderName { get; set; }

        [ExportColumn(Name = "Country", Order = 6)]
        public string Country { get; set; }

        [ExportColumn(Name = "Age", Order = 7)]
        public int Age { get; set; }
    }
}
