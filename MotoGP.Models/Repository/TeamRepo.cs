using MotoGP.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGP.Models.Repository
{
    public class TeamRepo : GenericRepository<Team>, ITeamRepo
    {
        public TeamRepo(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
