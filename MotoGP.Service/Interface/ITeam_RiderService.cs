using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoGP.ViewModels.Team_Rider;

namespace MotoGP.Service.Interface
{
    public interface ITeam_RiderService
    {
        void Create(Team_RiderCreateViewModel createVM);
    }
}
