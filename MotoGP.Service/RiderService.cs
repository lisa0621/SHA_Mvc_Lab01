using MotoGP.Models.Interface;
using MotoGP.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoGP.ViewModels.Rider;
using AutoMapper;
using MotoGP.Models;

namespace MotoGP.Service
{
    public class RiderService : IRiderService
    {
        private readonly IRiderRepo riderRepo;

        public RiderService(
            IRiderRepo riderRepo)
        {
            this.riderRepo = riderRepo;
        }

        public void Create(RiderCreateViewModel createVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RiderCreateViewModel, Rider>());
            var mapper = config.CreateMapper();
            var rider = mapper.Map<Rider>(createVM);

            riderRepo.Create(rider);
            riderRepo.UnitOfWork.Commit();
        }

        public List<RiderItemViewModel> GetList()
        {
            var data = riderRepo.All();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Rider, RiderItemViewModel>());
            var mapper = config.CreateMapper();
            var result = mapper.Map<List<RiderItemViewModel>>(data);

            return result;
        }
    }
}
