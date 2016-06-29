using LinqToExcel;
using MotoGP.Models;
using MotoGP.Models.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SHA_Mvc_Lab01.Infrastructure
{
    public class ImportDataHelper
    {
        /// <summary>
        /// 檢查匯入的 Excel 資料.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="importZipCodes">The import zip codes.</param>
        /// <returns></returns>
        public CheckResult CheckImportData(
            string fileName,
            List<Team> importTeams)
        {
            var result = new CheckResult();

            var targetFile = new FileInfo(fileName);

            if (!targetFile.Exists)
            {
                result.ID = Guid.NewGuid();
                result.Success = false;
                result.ErrorCount = 0;
                result.ErrorMessage = "匯入的資料檔案不存在";
                return result;
            }

            var excelFile = new ExcelQueryFactory(fileName);

            //欄位對映
            excelFile.AddMapping<Team>(x => x.Year, "Year");
            excelFile.AddMapping<Team>(x => x.Name, "Name");
            excelFile.AddTransformation<Team>(x => x.Class, cellValue => (EnumGameClass)Convert.ToInt32(cellValue));
            //excelFile.AddMapping<Team>(x => x.Class, "Class");
            excelFile.AddMapping<Team>(x => x.Bike, "Bike");
            excelFile.AddMapping<Team>(x => x.cc, "cc");
            excelFile.AddTransformation<Team>(x => x.Company, cellValue => (EnumBikeCompany)Convert.ToInt32(cellValue));
            //excelFile.AddMapping<Team>(x => x.Company, "Company");

            //SheetName
            var excelContent = excelFile.Worksheet<Team>("Sheet1");

            int errorCount = 0;
            int rowIndex = 1;
            var importErrorMessages = new List<string>();

            //檢查資料
            foreach (var row in excelContent)
            {
                var errorMessage = new StringBuilder();
                var team = new Team();

                team.Year = row.Year;
                //team.Name = row.Name;
                team.Class = row.Class;
                team.Bike = row.Bike;
                team.cc = row.cc;
                team.Company = row.Company;

                //Name
                if (string.IsNullOrWhiteSpace(row.Name))
                {
                    errorMessage.Append("Name - 不可空白. ");
                }
                team.Name = row.Name;

                //=============================================================================
                if (errorMessage.Length > 0)
                {
                    errorCount += 1;
                    importErrorMessages.Add(string.Format(
                        "第 {0} 列資料發現錯誤：{1}{2}",
                        rowIndex,
                        errorMessage,
                        "<br/>"));
                }
                importTeams.Add(team);
                rowIndex += 1;
            }

            try
            {
                result.ID = Guid.NewGuid();
                result.Success = errorCount.Equals(0);
                result.RowCount = importTeams.Count;
                result.ErrorCount = errorCount;

                string allErrorMessage = string.Empty;

                foreach (var message in importErrorMessages)
                {
                    allErrorMessage += message;
                }

                result.ErrorMessage = allErrorMessage;

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Saves the import data.
        /// </summary>
        /// <param name="importTeams">The import team codes.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SaveImportData(IEnumerable<Team> importTeams)
        {
            try
            {
                //先砍掉全部資料
                //using (var db = new MotoGPEntities())
                //{
                //    foreach (var item in db.Team.OrderBy(x => x.Id))
                //    {
                //        db.Team.Remove(item);
                //    }
                //    db.SaveChanges();
                //}

                //再把匯入的資料給存到資料庫
                using (var db = new MotoGPEntities())
                {
                    foreach (var item in importTeams)
                    {
                        db.Team.Add(item);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}