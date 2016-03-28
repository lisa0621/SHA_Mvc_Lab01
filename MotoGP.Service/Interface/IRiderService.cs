using MotoGP.ViewModels.Rider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGP.Service.Interface
{
    public interface IRiderService
    {
        List<RiderItemViewModel> GetList();
        void Create(RiderCreateViewModel createVM);
        RiderEditViewModel FindEditById(int id);
        void Edit(RiderEditViewModel editVM);
    }
}
