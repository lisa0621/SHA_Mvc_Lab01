using MotoGP.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHA_Mvc_Lab01.Controllers
{
    public class Team_RiderController : Controller
    {
        private readonly ITeamService teamService;
        private readonly IRiderService riderService;

        public Team_RiderController(
            ITeamService teamService,
            IRiderService riderService)
        {
            this.teamService = teamService;
            this.riderService = riderService;
        }

        // GET: Team_Rider
        public ActionResult Index()
        {
            return View();
        }

        // GET: Team_Rider/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Team_Rider/Create
        public ActionResult Create()
        {
            ViewBag.TeamList = new SelectList(teamService.GetList(), "Id", "Name");
            ViewBag.RiderList = new SelectList(riderService.GetList(), "Id", "Name");

            return View();
        }

        // POST: Team_Rider/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Team_Rider/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Team_Rider/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Team_Rider/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Team_Rider/Delete/5
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
