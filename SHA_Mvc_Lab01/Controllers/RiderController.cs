using AutoMapper;
using MotoGP.Service.Interface;
using MotoGP.ViewModels.Rider;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SHA_Mvc_Lab01.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
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
            //匯出資料欄位
            var exportColumns = ExportColumnAttributeHelper<RiderExportViewModel>.GetExportColumns()
                    .Select(c => new SelectListItem()
                    {
                        Value = c.ColumnName,
                        Text = c.Name,
                        Selected = true
                    })
                    .ToList();

            ViewBag.ExportColumns = exportColumns;
            
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

        [HttpPost]
        public ActionResult HasData()
        {
            JObject jo = new JObject();
            bool result = !riderService.GetList().Count.Equals(0);
            jo.Add("Msg", result.ToString());
            return Content(JsonConvert.SerializeObject(jo), "application/json");
        }

        public ActionResult Export(string fileName, string selectedColumns)
        {
            //要匯出的資料
            var data = riderService.GetList();

            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<RiderItemViewModel, RiderExportViewModel>()
                   .ForMember(d => d.Sex, o => o.MapFrom(s => s.Sex ? "Male" : "Female")));

            var mapper = config.CreateMapper();
            var result = mapper.Map<List<RiderExportViewModel>>(data);

            var dt = ExportDataHelper.GetExportDataTable(result, selectedColumns);

            var exportFileName = string.IsNullOrWhiteSpace(fileName)
                ? string.Concat(
                    "RiderData_",
                    DateTime.Now.ToString("yyyyMMddHHmmss"),
                    ".xlsx")
                : string.Concat(fileName, ".xlsx");

            return new ExportExcelResult
            {
                SheetName = "Rider",
                FileName = exportFileName,
                ExportData = dt
            };
        }
    }
}