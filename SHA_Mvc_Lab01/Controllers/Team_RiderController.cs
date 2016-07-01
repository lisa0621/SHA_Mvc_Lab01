using MotoGP.Service.Interface;
using MotoGP.ViewModels.Team_Rider;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PagedList;
using SHA_Mvc_Lab01.Infrastructure;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SHA_Mvc_Lab01.Controllers
{
    public class Team_RiderController : Controller
    {
        private readonly ITeamService teamService;
        private readonly IRiderService riderService;
        private readonly ITeam_RiderService teamRiderService;

        public Team_RiderController(
            ITeamService teamService,
            IRiderService riderService,
            ITeam_RiderService teamRiderService)
        {
            this.teamService = teamService;
            this.riderService = riderService;
            this.teamRiderService = teamRiderService;
        }

        // GET: Team_Rider
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            //匯出資料欄位
            //ViewBag.ExportColumns =
            //    ExportDataHelper.Team_RiderExportColumns()
            //                    .Select(column => new SelectListItem()
            //                    {
            //                        Value = column.Key,
            //                        Text = column.Value,
            //                        Selected = true
            //                    }).ToList();

            //匯出資料欄位
            var exportColumns = ExportColumnAttributeHelper<Team_RiderExportViewModel>.GetExportColumns()
                                .Select(c => new SelectListItem()
                                {
                                    Value = c.ColumnName,
                                    Text = c.Name,
                                    Selected = true
                                }).ToList();
            ViewBag.ExportColumns = exportColumns;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.TeamNameSortParm = String.IsNullOrEmpty(sortOrder) ? "teamName_desc" : string.Empty;
            ViewBag.RiderNameSortParm = sortOrder == "riderName" ? "riderName_desc" : "riderName";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var data = teamRiderService.GetList(sortOrder, searchString);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(data.ToPagedList(pageNumber, pageSize));
        }

        // GET: Team_Rider/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Team_Rider/Create
        public ActionResult Create()
        {
            ViewBag.TeamList = new SelectList(teamService.GetList(), "Id", "YearName");
            ViewBag.RiderList = new SelectList(riderService.GetList(), "Id", "Name");

            return View();
        }

        // POST: Team_Rider/Create
        [HttpPost]
        public ActionResult Create(Team_RiderCreateViewModel createVM)
        {
            if (!ModelState.IsValid)
            {
                return View(createVM);
            }

            teamRiderService.Create(createVM);

            return RedirectToAction("Index");
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

        public ActionResult ExportData()
        {
            GridView gv = new GridView();
            gv.DataSource = teamRiderService.GetList(null, null);
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=TeamRider.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = string.Empty;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Index");
        }

        public ActionResult Export()
        {
            var query = teamRiderService.GetList(null, null);

            string output = new JavaScriptSerializer().Serialize(query);
            var dt = JsonConvert.DeserializeObject<DataTable>(output.ToString());

            var exportFileName = string.Concat(
                "TeamRider_",
                DateTime.Now.ToString("yyyyMMddHHmmss"),
                ".xlsx");

            return new ExportExcelResult
            {
                SheetName = "TeamRider",
                FileName = exportFileName,
                ExportData = dt
            };
        }

        [HttpPost]
        public ActionResult HasData()
        {
            JObject jo = new JObject();
            bool result = !teamRiderService.GetList(null, null).Count.Equals(0);
            jo.Add("Msg", result.ToString());
            return Content(JsonConvert.SerializeObject(jo), "application/json");
        }

        public ActionResult ExportCustomFilename(string fileName)
        {
            var query = teamRiderService.GetList(null, null);

            string output = new JavaScriptSerializer().Serialize(query);
            var dt = JsonConvert.DeserializeObject<DataTable>(output.ToString());

            var exportFileName = string.IsNullOrWhiteSpace(fileName)
                ? string.Concat("TeamRider_", DateTime.Now.ToString("yyyyMMddHHmmss"), ".xlsx")
                : string.Concat(fileName, ".xlsx");

            return new ExportExcelResult
            {
                SheetName = "TeamRider",
                FileName = exportFileName,
                ExportData = dt
            };
        }

        public ActionResult ExportSelectedColumns(string fileName, string selectedColumns)
        {
            var query = teamRiderService.GetList(null, null);

            string output = new JavaScriptSerializer().Serialize(query);
            var dt = JsonConvert.DeserializeObject<DataTable>(output.ToString());

            var exportFileName = string.IsNullOrWhiteSpace(fileName)
                ? string.Concat("TeamRider_", DateTime.Now.ToString("yyyyMMddHHmmss"), ".xlsx")
                : string.Concat(fileName, ".xlsx");

            var exportColumns = ExportColumnAttributeHelper<Team_RiderExportViewModel>.GetExportColumns()
                    .ToDictionary(x => x.ColumnName, y => y.Name);

            var removeColumnNames = ExportDataHelper.GetRemoveColumnNames(
                exportColumns,
                selectedColumns);

            return new ExportExcelResult
            {
                SheetName = "TeamRider",
                FileName = exportFileName,
                ExportData = dt,
                RemoveColumnNames = removeColumnNames
            };
        }
    }
}