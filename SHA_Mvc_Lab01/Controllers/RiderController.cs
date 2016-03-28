using MotoGP.Service.Interface;
using MotoGP.ViewModels.Rider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHA_Mvc_Lab01.Controllers
{
    public class RiderController : Controller
    {
        private readonly IRiderService riderService;

        public RiderController(
            IRiderService riderService)
        {
            this.riderService = riderService;
        }

        // GET: Rider
        public ActionResult Index()
        {
            var data = riderService.GetList();
            return View(data);
        }

        // GET: Rider/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rider/Create
        [HttpPost]
        public ActionResult Create(RiderCreateViewModel createVM)
        {
            if (!ModelState.IsValid)
            {
                return View(createVM);
            }

            riderService.Create(createVM);

            return RedirectToAction("Index");
        }

        // GET: Rider/Edit/5
        public ActionResult Edit(int id)
        {
            var result = riderService.FindEditById(id);
            return View(result);
        }

        // POST: Rider/Edit/5
        [HttpPost]
        public ActionResult Edit(RiderEditViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                return View(editVM);
            }

            try
            {
                // TODO: Add update logic here
                riderService.Edit(editVM);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(editVM);
            }
        }

        // GET: Rider/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rider/Delete/5
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
