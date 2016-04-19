using MotoGP.ViewModels.Team_Rider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGP.Service.Interface
{
    public interface ITeam_RiderService
    {
        List<Team_RiderItemViewModel> GetList();
    }
}
