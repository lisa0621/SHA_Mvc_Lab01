using MotoGP.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoGP.ViewModels.Team_Rider;
using MotoGP.Models.Interface;

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

        public List<Team_RiderItemViewModel> GetList()
        {
            var data = team_RiderRepo.All().Select(x => 
                new Team_RiderItemViewModel {
                    Year = x.Team.Year,
                    Class = x.Team.Class.Value,
                    TeamName = x.Team.Name,
                    Company = x.Team.Company.Value,
                    RiderName = x.Rider.Name,
                    Country = x.Rider.Country,
                    Age = DateTime.Now.Year - x.Rider.Birth.Year
                 }).ToList();

            return data;
        }
    }
}
