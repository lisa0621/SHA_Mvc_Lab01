using MotoGP.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGP.Models.Repository
{
    public class RiderRepo : GenericRepository<Rider>, IRiderRepo
    {
        public RiderRepo(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
