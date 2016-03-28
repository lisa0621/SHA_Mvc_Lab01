using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoGP.ViewModels.Team;

namespace MotoGP.Service.Interface
{
    public interface ITeamService
    {
        void Create(TeamCreateViewModel createVM);
        List<TeamItemViewModel> GetList();
        TeamEditViewModel FindEditById(int id);
        void Edit(TeamEditViewModel editVM);
        TeamDetailViewModel GetDetailById(int id);
        void DeleteById(int id);
    }
}
