﻿using AutoMapper;
using MotoGP.Models;
using MotoGP.Service.Interface;
using MotoGP.ViewModels.Team;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SHA_Mvc_Lab01.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        //public ActionResult Index()
        //{
        //    var data = teamService.GetList();
        //    return View(data);
        //}

        public ActionResult Index()
        {
            var result = TempData["errorTeams"];
            if (result == null)
            {
                result = teamService.GetList();
            }

            return View(result);
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

        private string fileSavedPath = WebConfigurationManager.AppSettings["UploadPath"];

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            JObject jo = new JObject();
            string result = string.Empty;

            if (file == null)
            {
                jo.Add("Result", false);
                jo.Add("Msg", "請上傳檔案!");
                result = JsonConvert.SerializeObject(jo);
                return Content(result, "application/json");
            }
            if (file.ContentLength <= 0)
            {
                jo.Add("Result", false);
                jo.Add("Msg", "請上傳正確的檔案.");
                result = JsonConvert.SerializeObject(jo);
                return Content(result, "application/json");
            }

            string fileExtName = Path.GetExtension(file.FileName).ToLower();

            if (!fileExtName.Equals(".xls", StringComparison.OrdinalIgnoreCase)
                &&
                !fileExtName.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                jo.Add("Result", false);
                jo.Add("Msg", "請上傳 .xls 或 .xlsx 格式的檔案");
                result = JsonConvert.SerializeObject(jo);
                return Content(result, "application/json");
            }

            try
            {
                var uploadResult = this.FileUploadHandler(file);

                jo.Add("Result", !string.IsNullOrWhiteSpace(uploadResult));
                jo.Add("Msg", !string.IsNullOrWhiteSpace(uploadResult) ? uploadResult : string.Empty);

                result = JsonConvert.SerializeObject(jo);
            }
            catch (Exception ex)
            {
                jo.Add("Result", false);
                jo.Add("Msg", ex.Message);
                result = JsonConvert.SerializeObject(jo);
            }
            return Content(result, "application/json");
        }

        /// <summary>
        /// Files the upload handler.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">file;上傳失敗：沒有檔案！</exception>
        /// <exception cref="System.InvalidOperationException">上傳失敗：檔案沒有內容！</exception>
        private string FileUploadHandler(HttpPostedFileBase file)
        {
            string result;

            if (file == null)
            {
                throw new ArgumentNullException("file", "上傳失敗：沒有檔案！");
            }
            if (file.ContentLength <= 0)
            {
                throw new InvalidOperationException("上傳失敗：檔案沒有內容！");
            }

            try
            {
                string virtualBaseFilePath = Url.Content(fileSavedPath);
                string filePath = HttpContext.Server.MapPath(virtualBaseFilePath);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string newFileName = string.Concat(
                    DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    Path.GetExtension(file.FileName).ToLower());

                string fullFilePath = Path.Combine(Server.MapPath(fileSavedPath), newFileName);
                file.SaveAs(fullFilePath);

                result = newFileName;
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        [HttpPost]
        public ActionResult Import(string savedFileName)
        {
            var jo = new JObject();
            string result;

            try
            {
                var fileName = string.Concat(Server.MapPath(fileSavedPath), "/", savedFileName);

                var importTeams = new List<Team>();

                var helper = new ImportDataHelper();
                var checkResult = helper.CheckImportData(fileName, importTeams);

                jo.Add("Result", checkResult.Success);
                jo.Add("Msg", checkResult.Success ? string.Empty : checkResult.ErrorMessage);

                if (checkResult.Success)
                {
                    //儲存匯入的資料
                    helper.SaveImportData(importTeams);
                }
                result = JsonConvert.SerializeObject(jo);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Content(result, "application/json");
        }

        [HttpPost]
        public ActionResult ImportExam(string savedFileName)
        {
            var jo = new JObject();
            string result;

            try
            {
                var errorTeams = new List<TeamItemViewModel>();

                var fileName = string.Concat(Server.MapPath(fileSavedPath), "/", savedFileName);

                var importTeams = new List<Team>();

                var helper = new ImportDataHelper();
                var checkResult = helper.CheckImportData(fileName, importTeams);

                jo.Add("Result", checkResult.Success);
                jo.Add("Msg", checkResult.Success ? string.Empty : checkResult.ErrorMessage);

                if (checkResult.Success)
                {
                    //儲存匯入的資料
                    helper.SaveImportData(importTeams);
                }
                else
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Team, TeamItemViewModel>());
                    var mapper = config.CreateMapper();
                    errorTeams = mapper.Map<List<TeamItemViewModel>>(importTeams);

                    TempData["errorTeams"] = errorTeams;
                }

                result = JsonConvert.SerializeObject(jo);
            }
            catch (Exception ex)
            {
                throw;
            }

            return Content(result, "application/json");
        }

        [HttpPost]
        public ActionResult HasData()
        {
            JObject jo = new JObject();
            bool result = !teamService.GetList().Count.Equals(0);
            jo.Add("Msg", result.ToString());
            return Content(JsonConvert.SerializeObject(jo), "application/json");
        }

        public ActionResult ExportData()
        {
            GridView gv = new GridView();
            gv.DataSource = teamService.GetList();
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
            var query = teamService.GetList();

            string output = new JavaScriptSerializer().Serialize(query);
            var dt = JsonConvert.DeserializeObject<DataTable>(output.ToString());

            var exportFileName = string.Concat(
                "Team_",
                DateTime.Now.ToString("yyyyMMddHHmmss"),
                ".xlsx");

            return new ExportExcelResult
            {
                SheetName = "Team",
                FileName = exportFileName,
                ExportData = dt
            };
        }
    }
}