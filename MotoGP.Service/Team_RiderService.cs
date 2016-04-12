using MotoGP.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoGP.ViewModels.Team_Rider;
using MotoGP.Models.Interface;
using MotoGP.Models;
using AutoMapper;

namespace MotoGP.Service
{
    public class Team_RiderService : ITeam_RiderService
    {
        private readonly ITeam_RiderRepo teamRiderRepo;

        public Team_RiderService(ITeam_RiderRepo teamRiderRepo)
        {
            this.teamRiderRepo = teamRiderRepo;
        }

        public void Create(Team_RiderCreateViewModel createVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Team_RiderCreateViewModel, Team_Rider>());
            var mapper = config.CreateMapper();
            var teamRider = mapper.Map<Team_Rider>(createVM);

            teamRiderRepo.Create(teamRider);
            teamRiderRepo.UnitOfWork.Commit();
        }
    }
}
