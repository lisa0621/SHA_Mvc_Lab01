using MotoGP.Models.Interface;
using MotoGP.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoGP.ViewModels.Team;
using AutoMapper;
using MotoGP.Models;
using MotoGP.Models.Enum;

namespace MotoGP.Service
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepo teamRepo;

        public TeamService(
            ITeamRepo teamRepo)
        {
            this.teamRepo = teamRepo;
        }

        public void Create(TeamCreateViewModel createVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TeamCreateViewModel, Team>());
            var mapper = config.CreateMapper();
            var team = mapper.Map<Team>(createVM);

            teamRepo.Create(team);
            teamRepo.UnitOfWork.Commit();
        }

        public void DeleteById(int id)
        {
            teamRepo.DeleteById(id);
            teamRepo.UnitOfWork.Commit();
        }

        public void Edit(TeamEditViewModel editVM)
        {
            var data = teamRepo.GetDataById(editVM.Id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TeamEditViewModel, Team>());
            var mapper = config.CreateMapper();
            var team = mapper.Map(editVM, data);
            teamRepo.UnitOfWork.Commit();
        }

        public TeamEditViewModel FindEditById(int id)
        {
            var data = teamRepo.GetDataById(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Team, TeamEditViewModel>());
            var mapper = config.CreateMapper();
            var result = mapper.Map<TeamEditViewModel>(data);

            result.Class = (EnumGameClass)data.Class;
            result.Company = (EnumBikeCompany)data.Company;

            return result;
        }

        public TeamDetailViewModel GetDetailById(int id)
        {
            var data = teamRepo.GetDataById(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Team, TeamDetailViewModel>());
            var mapper = config.CreateMapper();
            var result = mapper.Map<TeamDetailViewModel>(data);

            return result;
        }

        public List<TeamItemViewModel> GetList()
        {
            var data = teamRepo.All();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Team, TeamItemViewModel>());
            var mapper = config.CreateMapper();
            var result = mapper.Map<List<TeamItemViewModel>>(data);

            return result;
        }
    }
}
