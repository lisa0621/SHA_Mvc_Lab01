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
using MotoGP.Models.Enum;
using MotoGP.ViewModels.Home;

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

        public void Edit(RiderEditViewModel editVM)
        {
            var data = riderRepo.GetDataById(editVM.Id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RiderEditViewModel, Rider>());
            var mapper = config.CreateMapper();
            var team = mapper.Map(editVM, data);
            riderRepo.UnitOfWork.Commit();
        }

        public RiderEditViewModel FindEditById(int id)
        {
            var data = riderRepo.GetDataById(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Rider, RiderEditViewModel>());
            var mapper = config.CreateMapper();
            var result = mapper.Map<RiderEditViewModel>(data);

            result.Classes = (EnumFlagGameClasses)data.Classes;

            return result;
        }

        public List<RiderItemViewModel> GetList()
        {
            var data = riderRepo.All();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Rider, RiderItemViewModel>());
            var mapper = config.CreateMapper();
            var result = mapper.Map<List<RiderItemViewModel>>(data);

            return result;
        }

        public List<MotogpGroup> GetMotogpGroupList()
        {
            //IQueryable<MotogpGroup> data = from rider in riderRepo.All()
            //                               group rider by rider.Classes into dateGroup
            //                                       select new MotogpGroup()
            //                                       {
            //                                           Classes = dateGroup.Key,
            //                                           RiderCount = dateGroup.Count()
            //                                       };

            //var data = from p in Enum.GetValues(typeof(EnumFlagGameClasses)).Cast<EnumFlagGameClasses>()
            //           from rider in riderRepo.All()
            //           where ((EnumFlagGameClasses)rider.Classes).HasFlag(p)
            //           group rider.Classes by p into dateGroup
            //           select new MotogpGroup()
            //           {
            //               Classes = dateGroup.Key,
            //               RiderCount = dateGroup.Count()
            //           };

            //var data  = from p in Enum.GetValues(typeof(EnumFlagGameClasses)).Cast<EnumFlagGameClasses>()
            //                      let temp = riderRepo.All().Where(q => ((EnumFlagGameClasses)q.Classes & p) == p).Select(x => x.Id).ToArray()
            //                      where temp.Length > 0
            //                      select new MotogpGroup(){ Classes = p, RiderCount = temp.Count() };

            var data = from p in Enum.GetValues(typeof(EnumFlagGameClasses)).Cast<EnumFlagGameClasses>()
                      select new MotogpGroup() { Classes = p, RiderCount = riderRepo.All().Where(q => ((EnumFlagGameClasses)q.Classes & p) == p).Count() };
            return data.ToList();
        }
    }
}
