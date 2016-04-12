using MotoGP.Service.Interface;
using MotoGP.ViewModels.Team;
using System.Web.Mvc;

namespace SHA_Mvc_Lab01.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService teamService;

        public TeamController(
            ITeamService teamService)
        {
            this.teamService = teamService;
        }

        // GET: Team
        public ActionResult Index()
        {
            var data = teamService.GetList();
            return View(data);
        }

        // GET: Team/Details/5
        public ActionResult Details(int id)
        {
            var data = teamService.GetDetailById(id);
            return View(data);
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
        [HttpPost]
        public ActionResult Create(TeamCreateViewModel createVM)
        {
            if (!ModelState.IsValid)
            {
                return View(createVM);
            }

            teamService.Create(createVM);

            return RedirectToAction("Index");
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int id)
        {
            var result = teamService.FindEditById(id);
            return View(result);
        }

        // POST: Team/Edit/5
        [HttpPost]
        public ActionResult Edit(TeamEditViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                return View(editVM);
            }

            try
            {
                // TODO: Add update logic here
                teamService.Edit(editVM);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(editVM);
            }
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int id)
        {
            teamService.DeleteById(id);
            return RedirectToAction("Index");
        }

        // POST: Team/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}