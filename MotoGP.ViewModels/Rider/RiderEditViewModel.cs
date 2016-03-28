using MotoGP.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGP.ViewModels.Rider
{
    public class RiderEditViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birth { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public bool Sex { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        [DataType(DataType.MultilineText)]
        public string Introduction { get; set; }

        public EnumFlagGameClasses Classes { get; set; }
    }
}
