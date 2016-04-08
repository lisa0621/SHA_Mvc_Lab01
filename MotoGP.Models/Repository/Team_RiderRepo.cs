using MotoGP.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGP.Models.Repository
{
    public class Team_RiderRepo : GenericRepository<Team_Rider>, ITeam_RiderRepo
    {
        public Team_RiderRepo(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
