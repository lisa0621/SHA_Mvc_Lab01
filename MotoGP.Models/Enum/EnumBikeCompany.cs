using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoGP.Resources.ViewModels.Team;

namespace MotoGP.Models.Enum
{
    public enum EnumBikeCompany
    {
        [Display(Name = "enumBikeCompanyAprilia", ResourceType = typeof(TeamResources))]
        Aprilia = 1,
        [Display(Name = "enumBikeCompanyDucati", ResourceType = typeof(TeamResources))]
        Ducati = 2,
        [Display(Name = "enumBikeCompanyHonda", ResourceType = typeof(TeamResources))]
        Honda = 3,
        [Display(Name = "enumBikeCompanyYamaha", ResourceType = typeof(TeamResources))]
        Yamaha = 4,
        [Display(Name = "enumBikeCompanySuzuki", ResourceType = typeof(TeamResources))]
        Suzuki = 5
    }
}
