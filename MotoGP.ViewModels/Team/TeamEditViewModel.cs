using MotoGP.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGP.ViewModels.Team
{
    public class TeamEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "請填寫年份")]
        public int Year { get; set; }

        [Required(ErrorMessage = "請填隊伍名稱")]
        public string Name { get; set; }

        public EnumGameClass Class { get; set; }

        public string Bike { get; set; }

        public int cc { get; set; }

        public EnumBikeCompany Company { get; set; }
    }
}
