using MotoGP.Models.Enum;
using SHA_Mvc_Lab01.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGP.ViewModels.Rider
{
    public class RiderExportViewModel
    {
        public int Id { get; set; }

        [ExportColumn(Name = "Name", Order = 1)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [ExportColumn(Name = "Birth", Order = 3)]
        public DateTime Birth { get; set; }

        [ExportColumn(Name = "Country", Order = 4)]
        public string Country { get; set; }

        [ExportColumn(Name = "City", Order = 5)]
        public string City { get; set; }

        [ExportColumn(Name = "Sex", Order = 6)]
        public string Sex { get; set; }

        [ExportColumn(Name = "Weight", Order = 7)]
        public double Weight { get; set; }

        [ExportColumn(Name = "Height", Order = 8)]
        public double Height { get; set; }

        [ExportColumn(Name = "Introduction", Order = 9)]
        public string Introduction { get; set; }

        [ExportColumn(Name = "Classes", Order = 2)]
        public EnumFlagGameClasses Classes { get; set; }
    }
}
