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
        private readonly IRiderRepo riderRepo;
        private readonly ITeamRepo teamRepo;
        private readonly ITeam_RiderRepo team_RiderRepo;

        public Team_RiderService(
            ITeamRepo teamRepo,
            IRiderRepo riderRepo,
            ITeam_RiderRepo team_RiderRepo
            )
        {
            this.teamRepo = teamRepo;
            this.riderRepo = riderRepo;
            this.team_RiderRepo = team_RiderRepo;
        }

        public List<Team_RiderItemViewModel> GetList(string sortOrder)
        {
            var teamRiders = team_RiderRepo.All().Select(x => 
                new Team_RiderItemViewModel {
                    Year = x.Team.Year,
                    Class = x.Team.Class.Value,
                    TeamName = x.Team.Name,
                    Company = x.Team.Company.Value,
                    RiderName = x.Rider.Name,
                    Country = x.Rider.Country,
                    Age = DateTime.Now.Year - x.Rider.Birth.Year
                 });

            switch (sortOrder)
            {
                case "teamName_desc":
                    teamRiders = teamRiders.OrderByDescending(s => s.TeamName);
                    break;
                case "riderName":
                    teamRiders = teamRiders.OrderBy(s => s.RiderName);
                    break;
                case "riderName_desc":
                    teamRiders = teamRiders.OrderByDescending(s => s.RiderName);
                    break;
                default:
                    teamRiders = teamRiders.OrderBy(s => s.TeamName);
                    break;
            }


            return teamRiders.ToList();
        }

        public void Create(Team_RiderCreateViewModel createVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Team_RiderCreateViewModel, Team_Rider>());
            var mapper = config.CreateMapper();
            var teamRider = mapper.Map<Team_Rider>(createVM);
           
            team_RiderRepo.Create(teamRider);
            team_RiderRepo.UnitOfWork.Commit();
        }
    }
}
